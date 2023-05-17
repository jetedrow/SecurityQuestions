using MediatR;
using SecurityQuestions.Data;
using SecurityQuestions.Features.Answer;
using SecurityQuestions.Features.QuestionStore;
using Spectre.Console;
using System.Net.Security;

namespace SecurityQuestions.Console;

public class AppCore
{
    private readonly IMediator mediator;

    public AppCore(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public enum FlowResult
    {
        NameFlow,
        StoreFlow,
        AnswerFlow,

    }

    public async Task RunAsync()
    {
        FlowResult currentFlow = FlowResult.NameFlow;
        ICollection<UserQuestion> userQuestions = new List<UserQuestion>();
        string currentName = String.Empty;

        //TODO: Add an exit condition so the application can close gracefully.
        while (true)
        {
            switch (currentFlow)
            {
                case FlowResult.NameFlow:
                    (currentFlow, currentName, userQuestions) = await NameFlow();
                    break;
                case FlowResult.StoreFlow:
                    currentFlow = await StoreFlow(currentName);
                    break;
                case FlowResult.AnswerFlow:
                    currentFlow = await AnswerFlow(userQuestions);
                    break;
                default:
                    currentFlow = FlowResult.NameFlow;
                    break;
            }
        }
    }

    public async Task<(FlowResult, string Name, ICollection<UserQuestion> userQuestions)> NameFlow()
    {
        AnsiConsole.Clear();

        var name = AnsiConsole.Ask<string>("Hi, what is your name?");

        var availableQuestions = await mediator.Send(new RetrieveQuestionsByNameRequest { Name = name });

        if (!availableQuestions.Any())
        {
            // No questions available for user, or user not entered.  Prompt to store questions for user.
            return (FlowResult.StoreFlow, name, availableQuestions);
        }

        if (AnsiConsole.Confirm("Do you want to answer a security question?"))
        {
            return (FlowResult.AnswerFlow, name, availableQuestions);
        }

        return (FlowResult.StoreFlow, name, availableQuestions);
    }

    public async Task<FlowResult> StoreFlow(string name)
    {
        if (AnsiConsole.Confirm("Would you like to store answers to security questions?"))
        {
            // We do want to store answers.
            var allQuestions = await mediator.Send(new ListAllSecurityQuestionsRequest());

            AnsiConsole.Clear();
            var questions = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                .Title("Please select which questions you would like to answer (you must select a minimum of 3).")
                .MoreChoicesText("[grey]Arrow up and down to see additional questions.[/]")
                .InstructionsText("[grey](Press [blue]<space>[/] to select a question, [green]<enter>[/] to begin answering them)[/]")
                .AddChoices(allQuestions.Select(q => q.Question))
                .HighlightStyle(new Style (Color.Yellow))
                .PageSize(8)
                );

            // If they chose 0 questions, we assume they do not want to answer any of them.
            if (!questions.Any())
            {
                return FlowResult.NameFlow;
            }

            if (questions.Count < 3)
            {
                AnsiConsole.WriteLine("You must choose at least 3 questions to answer.");
                return FlowResult.StoreFlow;
            }

            AnsiConsole.Clear();

            var storeRequest = new StoreUserQuestionsRequest() { Name = name };

            foreach (var question in questions)
            {

                var answer = AnsiConsole.Prompt(
                    new TextPrompt<string>(question)
                        .ValidationErrorMessage("[red]You must provide an answer to the question[/]")
                        .Validate(answer =>
                            {
                                if (answer.Trim().Length > 0)
                                {
                                    return ValidationResult.Success();
                                }
                                return ValidationResult.Error();
                            }));

                var selectedQuestionId = allQuestions.First(q => q.Question == question).Id;

                storeRequest.QuestionAnswers.Add(new QuestionAnswer(selectedQuestionId, answer));
            }

            await mediator.Send(storeRequest);

        }

        return FlowResult.NameFlow;
    }

    public static async Task<FlowResult> AnswerFlow(ICollection<UserQuestion> userQuestions)
    {
        await Task.Yield();

        foreach (var userQuestion in userQuestions)
        {
            if (userQuestion.QuestionText is not null && userQuestion.Answer is not null)
            {
                var userAnswer = AnsiConsole.Ask<string>(userQuestion.QuestionText);
                if (userAnswer.ToLower() == userQuestion.Answer.ToLower()) 
                {
                    // Our answer matches!
                    AnsiConsole.MarkupLine("\n[green]Congratulations! You answered the question correctly![/]");
                    AnsiConsole.Write("\nPress <enter> to continue...");
                    System.Console.ReadLine();
                    return FlowResult.NameFlow;
                }
                else
                {
                    // No match.
                    AnsiConsole.MarkupLine("[red]Sorry, this answer is incorrect![/]\n");
                }
            }
        }
        // We have tried all configured questions, let the user know they ran out of questions.
        AnsiConsole.MarkupLine("[red]You have run out of questions to answer, please try again later.[/]");
        AnsiConsole.Write("\nPress <enter> to continue...");
        System.Console.ReadLine();
        return FlowResult.NameFlow;

    }
}
