using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyQuestionsAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_question_id",
                table: "Answers",
                column: "question_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_question_id",
                table: "Answers",
                column: "question_id",
                principalTable: "Questions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers",
                column: "author_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_question_id",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_question_id",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers",
                column: "author_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
