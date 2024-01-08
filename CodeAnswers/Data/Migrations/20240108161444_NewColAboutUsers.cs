using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewColAboutUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "about",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "about",
                table: "Users");
        }
    }
}
