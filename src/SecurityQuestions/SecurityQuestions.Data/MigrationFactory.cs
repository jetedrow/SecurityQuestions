using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.Data
{
    public class QuestionContextFactory : IDesignTimeDbContextFactory<QuestionContext>
    {
        public QuestionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuestionContext>();
                optionsBuilder.UseSqlite("Data Source=:memory:;New=True;");

            return new QuestionContext(optionsBuilder.Options);
        }
    }
}
