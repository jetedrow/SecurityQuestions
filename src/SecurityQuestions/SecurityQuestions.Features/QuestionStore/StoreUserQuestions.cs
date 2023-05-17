using MediatR;
using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Features.QuestionStore
{
    public class StoreUserQuestionsRequest : IRequest
    {
        public string Name { get; set; } = "";
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
    }

    public record QuestionAnswer(int QuestionId, string Answer);

    public class StoreUserQuestionsHandler : IRequestHandler<StoreUserQuestionsRequest>
    {
        private readonly QuestionContext context;

        public StoreUserQuestionsHandler(QuestionContext context)
        {
            this.context = context;
        }

        public async Task Handle(StoreUserQuestionsRequest request, CancellationToken cancellationToken)
        {
            // Remove any user data previously stored.
            var user = context.Users.FirstOrDefault(u => u.Name == request.Name.ToLower());
            if (user != null) 
            {
                context.Remove(user);
            }

            var newUser = new Models.User()
            {
                Name = request.Name.ToLower(),
                Questions = request.QuestionAnswers
                    .Select(a => new Models.UserQuestion { SecurityQuestionId = a.QuestionId, Answer = a.Answer.ToLower()})
                    .ToList()
            };

            context.Users.Add(newUser);

            await context.SaveChangesAsync();

        }
    }
}
