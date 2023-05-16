using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityQuestions.Models
{
    public class UserQuestion
    {
        public int UserId { get; set; }

        public int SecurityQuestionId { get; set; }

        public SecurityQuestion? Question { get; set; }

        public string? Answer { get; set; }

    }
}
