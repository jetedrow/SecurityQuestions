using Microsoft.EntityFrameworkCore;
using SecurityQuestions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityQuestions.Data
{
    public class QuestionContext : DbContext
    {
        public QuestionContext(DbContextOptions<QuestionContext> options) : base(options) { }

        public QuestionContext() { }

        public DbSet<User> Users { get; set; }

        public DbSet<SecurityQuestion> SecurityQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u=> u.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Questions)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SecurityQuestion>()
                .Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<SecurityQuestion>()
                .HasData(
                    new SecurityQuestion { SecurityQuestionId = 1, QuestionText = "In what city were you born?" },
                    new SecurityQuestion { SecurityQuestionId = 2, QuestionText = "What is the name of your favorite pet?" },
                    new SecurityQuestion { SecurityQuestionId = 3, QuestionText = "What is your mother's maiden name?" },
                    new SecurityQuestion { SecurityQuestionId = 4, QuestionText = "What high school did you attend?" },
                    new SecurityQuestion { SecurityQuestionId = 5, QuestionText = "What was the mascot of your high school?" },
                    new SecurityQuestion { SecurityQuestionId = 6, QuestionText = "What was the make of your first car?" },
                    new SecurityQuestion { SecurityQuestionId = 7, QuestionText = "What was your favorite toy as a child?" },
                    new SecurityQuestion { SecurityQuestionId = 8, QuestionText = "Where did you meet your spouse?" },
                    new SecurityQuestion { SecurityQuestionId = 9, QuestionText = "What is your favorite meal?" },
                    new SecurityQuestion { SecurityQuestionId = 10, QuestionText = "What is the first name of the best man at your wedding?" }
                );

            modelBuilder.Entity<UserQuestion>()
                .HasKey(k => new { k.UserId, k.SecurityQuestionId });

        }

    }
}
