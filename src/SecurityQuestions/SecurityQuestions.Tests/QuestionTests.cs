using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using SecurityQuestions.Features.Answer;
using FluentAssertions;
using SecurityQuestions.Features.QuestionStore;

namespace SecurityQuestions.Tests;
public class QuestionTests
{
    public QuestionContext context;

    [SetUp]
    public void Setup()
    {
        context = new QuestionContextFactory().CreateDbContext(Array.Empty<string>());
        context.Database.Migrate();
    }

    [Test]
    public async Task EnsureQuestionListAddedInMigrations()
    {
        var listHandler = new ListAllSecurityQuestionsHandler(context);
        var cancelToken = new CancellationToken();

        var request = new ListAllSecurityQuestionsRequest();


        var response = await listHandler.Handle(request, cancelToken);
        response.Should().NotBeNull().And.HaveCount(10);

    }

}