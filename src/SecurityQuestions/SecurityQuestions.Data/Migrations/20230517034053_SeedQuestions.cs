using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityQuestions.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SecurityQuestions",
                columns: new[] { "SecurityQuestionId", "QuestionText" },
                values: new object[,]
                {
                    { 1, "In what city were you born?" },
                    { 2, "What is the name of your favorite pet?" },
                    { 3, "What is your mother's maiden name?" },
                    { 4, "What high school did you attend?" },
                    { 5, "What was the mascot of your high school?" },
                    { 6, "What was the make of your first car?" },
                    { 7, "What was your favorite toy as a child?" },
                    { 8, "Where did you meet your spouse?" },
                    { 9, "What is your favorite meal?" },
                    { 10, "What is the first name of the best man at your wedding?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SecurityQuestions",
                keyColumn: "SecurityQuestionId",
                keyValue: 10);
        }
    }
}
