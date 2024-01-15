using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesForLikes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersRating_Users_UsersId",
                table: "AnswersRating");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsRating_Users_UsersId",
                table: "QuestionsRating");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsRating_UsersId",
                table: "QuestionsRating");

            migrationBuilder.DropIndex(
                name: "IX_AnswersRating_UsersId",
                table: "AnswersRating");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "QuestionsRating");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "AnswersRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "QuestionsRating",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "AnswersRating",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRating_UsersId",
                table: "QuestionsRating",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersRating_UsersId",
                table: "AnswersRating",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersRating_Users_UsersId",
                table: "AnswersRating",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRating_Users_UsersId",
                table: "QuestionsRating",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
