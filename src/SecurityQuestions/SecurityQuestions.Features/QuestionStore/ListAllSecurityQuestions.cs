using MediatR;
using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using SecurityQuestions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Features.QuestionStore
{
    public class ListAllSecurityQuestionsRequest : IRequest<IEnumerable<SecurityQuestion>>
    {
    }

    public record SecurityQuestion(int Id, string Question);

    public class ListAllSecurityQuestionsHandler : IRequestHandler<ListAllSecurityQuestionsRequest, IEnumerable<SecurityQuestion>>
    {
        private readonly QuestionContext context;

        public ListAllSecurityQuestionsHandler(QuestionContext context)
        {
            this.context = context;
        }
        public Task<IEnumerable<SecurityQuestion>> Handle(ListAllSecurityQuestionsRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.SecurityQuestions.Select(q => new SecurityQuestion(q.SecurityQuestionId, q.QuestionText)).AsEnumerable());

        }
    }
}
