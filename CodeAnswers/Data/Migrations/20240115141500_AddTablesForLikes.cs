using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesForLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswersRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    likes = table.Column<bool>(type: "bit", nullable: false),
                    dislikes = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    answer_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersRating", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnswersRating_Answers_answer_id",
                        column: x => x.answer_id,
                        principalTable: "Answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswersRating_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "QuestionsRating",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    likes = table.Column<bool>(type: "bit", nullable: false),
                    dislikes = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsRating", x => x.id);
                    table.ForeignKey(
                        name: "FK_QuestionsRating_Questions_question_id",
                        column: x => x.question_id,
                        principalTable: "Questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsRating_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersRating_answer_id",
                table: "AnswersRating",
                column: "answer_id");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersRating_user_id",
                table: "AnswersRating",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRating_question_id",
                table: "QuestionsRating",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsRating_user_id",
                table: "QuestionsRating",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersRating");

            migrationBuilder.DropTable(
                name: "QuestionsRating");
        }
    }
}
