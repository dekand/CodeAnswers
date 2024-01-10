using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColUsersReputation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "reputation",
                table: "Users",
                type: "int",
                nullable: false,
                computedColumnSql: "[dbo].[Fun_ReputationCalc]([id])",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "reputation",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[dbo].[Fun_ReputationCalc]([id])");
        }
    }
}
