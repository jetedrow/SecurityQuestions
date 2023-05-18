using MediatR;
using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using SecurityQuestions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Features.Answer
{
    public class RetrieveQuestionsByNameRequest : IRequest<ICollection<UserQuestion>>
    {
        public string Name { get; set; } = "";
    }

    public record UserQuestion(string? QuestionText, string? Answer);

    public class RetrieveQuestionsByNameHandler : IRequestHandler<RetrieveQuestionsByNameRequest, ICollection<UserQuestion>>
    {
        private readonly QuestionContext context;

        public RetrieveQuestionsByNameHandler(QuestionContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<UserQuestion>> Handle(RetrieveQuestionsByNameRequest request, CancellationToken cancellationToken)
        {
            var userData = await context.Users
                .Include(u => u.Questions)
                .ThenInclude(q => q.Question)
                .FirstOrDefaultAsync(u => u.Name.ToLower() == request.Name.ToLower(), cancellationToken: cancellationToken);

            return userData?.Questions?
                .Select(q => new UserQuestion(q.Question.QuestionText, q.Answer)).ToList() ?? Enumerable.Empty<UserQuestion>().ToList();
        }
    }
}
