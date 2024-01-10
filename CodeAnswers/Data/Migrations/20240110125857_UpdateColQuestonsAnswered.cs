using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColQuestonsAnswered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "answered",
                table: "Questions",
                type: "bit",
                nullable: false,
                computedColumnSql: "[dbo].[Fun_Answered]([id])",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "answered",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComputedColumnSql: "[dbo].[Fun_Answered]([id])");
        }
    }
}
