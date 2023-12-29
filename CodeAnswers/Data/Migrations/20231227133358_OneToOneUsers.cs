using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToOneUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "asp_net_users_id",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_asp_net_users_id",
                table: "Users",
                column: "asp_net_users_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_asp_net_users_id",
                table: "Users",
                column: "asp_net_users_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_asp_net_users_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_asp_net_users_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "asp_net_users_id",
                table: "Users");
        }
    }
}
