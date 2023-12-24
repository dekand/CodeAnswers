using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyUsersAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answers_author_id",
                table: "Answers",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers",
                column: "author_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_author_id",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_author_id",
                table: "Answers");
        }
    }
}
