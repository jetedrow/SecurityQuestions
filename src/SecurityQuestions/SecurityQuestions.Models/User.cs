using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityQuestions.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string? Name { get; set; }

        public ICollection<UserQuestion>? Questions { get; set; }
    }
}
