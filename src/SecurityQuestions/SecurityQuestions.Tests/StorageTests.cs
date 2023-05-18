using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using SecurityQuestions.Features.Answer;
using FluentAssertions;
using SecurityQuestions.Features.QuestionStore;

namespace SecurityQuestions.Tests;
public class StorageTests
{
    public QuestionContext context;

    [SetUp]
    public void Setup()
    {
        context = new QuestionContextFactory().CreateDbContext(Array.Empty<string>());
        context.Database.Migrate();
    }

    [Test]
    public async Task EnsureUserDataIsSavedWhenSubmitted()
    {
        var storeHandler = new StoreUserQuestionsHandler(context);
        var cancelToken = new CancellationToken();

        var request = new StoreUserQuestionsRequest
        {
            Name = "Jeff",
            QuestionAnswers = new List<QuestionAnswer>
            {
                new QuestionAnswer(2, "best"),
                new QuestionAnswer(4, "unit"),
                new QuestionAnswer(5, "test"),
                new QuestionAnswer(8, "ever")
            }
        };

        string[] validAnswers = new string[] { "best", "unit", "test", "ever" };
        
        await storeHandler.Awaiting(f => f.Handle(request, cancelToken)).Should().NotThrowAsync();

        context.Users.Should().HaveCount(1);
        context.Users.First().Questions.Should().HaveCount(4);

        context.Users.First().Questions.Select(q => q.Answer).Should().Contain(validAnswers);
    }

}