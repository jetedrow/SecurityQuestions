using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using SecurityQuestions.Features.Answer;
using FluentAssertions;

namespace SecurityQuestions.Tests;
public class NameTests
{
    public QuestionContext context;

    [SetUp]
    public void Setup()
    {
        context = new QuestionContextFactory().CreateDbContext(Array.Empty<string>());
        context.Database.Migrate();

        context.Users.Add(new Models.User
        {
            Name = "Test",
            Questions = new[]
            {
                new Models.UserQuestion { SecurityQuestionId = 1, Answer = "one"},
                new Models.UserQuestion { SecurityQuestionId = 2, Answer = "two"},
                new Models.UserQuestion { SecurityQuestionId = 3, Answer = "three"}
            }
        });

        context.SaveChanges();
    }

    [Test]
    public async Task EnsureAnyCapitalization()
    {
        var nameHandler = new RetrieveQuestionsByNameHandler(context);
        var cancelToken = new CancellationToken();

        var requestSame = new RetrieveQuestionsByNameRequest { Name = "Test" };
        var requestCapital = new RetrieveQuestionsByNameRequest { Name = "TEST" };
        var requestLower = new RetrieveQuestionsByNameRequest { Name = "test" };
        var requestOtherName = new RetrieveQuestionsByNameRequest { Name = "bogus" };


        var responseSame = await nameHandler.Handle(requestSame, cancelToken);
        responseSame.Should().NotBeNull().And.HaveCount(3);

        var responseCapital = await nameHandler.Handle(requestCapital, cancelToken);
        responseCapital.Should().NotBeNull().And.HaveCount(3);

        var responseLower = await nameHandler.Handle(requestLower, cancelToken);
        responseLower.Should().NotBeNull().And.HaveCount(3);

        var responseOtherName = await nameHandler.Handle(requestOtherName, cancelToken);
        responseOtherName.Should().NotBeNull().And.BeEmpty();

    }

    [Test]
    public async Task EnsureAllQuestionsAndAnswersMatch()
    {
        var nameHandler = new RetrieveQuestionsByNameHandler(context);
        var cancelToken = new CancellationToken();

        var request = new RetrieveQuestionsByNameRequest { Name = "Test" };


        var response = await nameHandler.Handle(request, cancelToken);
        response.Should().NotBeNull().And.HaveCount(3);

        response.ElementAt(0).Answer.Should().Be("one");
        response.ElementAt(0).QuestionText.Should().Be("In what city were you born?");

        response.ElementAt(1).Answer.Should().Be("two");
        response.ElementAt(1).QuestionText.Should().Be("What is the name of your favorite pet?");

        response.ElementAt(2).Answer.Should().Be("three");
        response.ElementAt(2).QuestionText.Should().Be("What is your mother's maiden name?");

    }
}