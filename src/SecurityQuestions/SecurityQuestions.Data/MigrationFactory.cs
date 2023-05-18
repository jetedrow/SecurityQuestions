using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SecurityQuestions.Data
{
    public class QuestionContextFactory : IDesignTimeDbContextFactory<QuestionContext>
    {
        public QuestionContext CreateDbContext(string[] args)
        {
            var dbName = ":memory:";

            if (args.Length > 0) dbName = args[0];

            var persistantConnection = new SqliteConnection($"Data Source={dbName};");
            persistantConnection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<QuestionContext>();
                optionsBuilder.UseSqlite(persistantConnection);

            return new QuestionContext(optionsBuilder.Options);
        }
    }
}
