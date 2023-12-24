using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyUsersQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_author_id",
                table: "Questions",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_author_id",
                table: "Questions",
                column: "author_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_author_id",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_author_id",
                table: "Questions");
        }
    }
}
