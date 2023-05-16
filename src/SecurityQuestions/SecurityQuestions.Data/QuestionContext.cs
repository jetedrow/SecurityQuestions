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
            
            modelBuilder.Entity<SecurityQuestion>()
                .Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(250);

            modelBuilder.Entity<UserQuestion>()
                .HasKey(k => new { k.UserId, k.SecurityQuestionId });

        }

    }
}
