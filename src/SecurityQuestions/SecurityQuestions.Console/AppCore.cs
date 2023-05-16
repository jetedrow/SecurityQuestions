using SecurityQuestions.Data;
using Spectre.Console;
using System.Net.Security;

namespace SecurityQuestions.Console;

public class AppCore
{
    public AppCore()
    {
    }

    public enum FlowResult
    {
        NameFlow,
        StoreFlow,
        AnswerFlow,

    }

    public void Run()
    {
        FlowResult currentFlow = FlowResult.NameFlow;

        //TODO: Add an exit condition so the application can close gracefully.
        while (true)
        {
            switch (currentFlow)
            {
                case FlowResult.NameFlow:
                    currentFlow = NameFlow();
                    break;
                case FlowResult.StoreFlow:
                    currentFlow = StoreFlow();
                    break;
                case FlowResult.AnswerFlow:
                    currentFlow = AnswerFlow();
                    break;
                default:
                    currentFlow = FlowResult.NameFlow;  
                    break;
            }
        }
    }

    public FlowResult NameFlow()
    {
        var name = AnsiConsole.Ask<string>("Hi, what is your name?");

        return FlowResult.NameFlow;
    }

    public FlowResult StoreFlow()
    {
        if (AnsiConsole.Confirm("Would you like to store answers to security questions?"))
        {
            // We do want to store answers.
            return FlowResult.NameFlow;
        }
        else
        {
            // We do not want to store answers.  Return to name flow.
            return FlowResult.NameFlow;
        }
        
    }

    public FlowResult AnswerFlow()
    {
        return FlowResult.NameFlow;
    }
}
