using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTablesForLikes4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "likes",
                table: "QuestionsRating",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "dislikes",
                table: "QuestionsRating",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "QuestionsRating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "likes",
                table: "AnswersRating",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "dislikes",
                table: "AnswersRating",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "AnswersRating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRating_user_id",
                table: "QuestionsRating",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersRating_user_id",
                table: "AnswersRating",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersRating_Users_user_id",
                table: "AnswersRating",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsRating_Users_user_id",
                table: "QuestionsRating",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersRating_Users_user_id",
                table: "AnswersRating");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsRating_Users_user_id",
                table: "QuestionsRating");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsRating_user_id",
                table: "QuestionsRating");

            migrationBuilder.DropIndex(
                name: "IX_AnswersRating_user_id",
                table: "AnswersRating");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "QuestionsRating");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "AnswersRating");

            migrationBuilder.AlterColumn<bool>(
                name: "likes",
                table: "QuestionsRating",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "dislikes",
                table: "QuestionsRating",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "likes",
                table: "AnswersRating",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "dislikes",
                table: "AnswersRating",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
