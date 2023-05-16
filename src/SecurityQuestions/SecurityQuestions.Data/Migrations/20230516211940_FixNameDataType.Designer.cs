﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SecurityQuestions.Data;

#nullable disable

namespace SecurityQuestions.Data.Migrations
{
    [DbContext(typeof(QuestionContext))]
    [Migration("20230516211940_FixNameDataType")]
    partial class FixNameDataType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("SecurityQuestions.Models.SecurityQuestion", b =>
                {
                    b.Property<int>("SecurityQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("SecurityQuestionId");

                    b.ToTable("SecurityQuestions");
                });

            modelBuilder.Entity("SecurityQuestions.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SecurityQuestions.Models.UserQuestion", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecurityQuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Answer")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "SecurityQuestionId");

                    b.HasIndex("SecurityQuestionId");

                    b.ToTable("UserQuestion");
                });

            modelBuilder.Entity("SecurityQuestions.Models.UserQuestion", b =>
                {
                    b.HasOne("SecurityQuestions.Models.SecurityQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("SecurityQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SecurityQuestions.Models.User", null)
                        .WithMany("Questions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SecurityQuestions.Models.User", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
