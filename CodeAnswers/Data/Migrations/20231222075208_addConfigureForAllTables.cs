using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class addConfigureForAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reputation",
                table: "Users",
                newName: "reputation");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Users",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RegistrationDate",
                table: "Users",
                newName: "registration_date");

            migrationBuilder.RenameColumn(
                name: "LinkSocial",
                table: "Users",
                newName: "link_social");

            migrationBuilder.RenameColumn(
                name: "LinkGithub",
                table: "Users",
                newName: "link_github");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tags",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "reputation",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "registration_date",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "none",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_name",
                table: "Users",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_name",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "reputation",
                table: "Users",
                newName: "Reputation");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "Users",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "registration_date",
                table: "Users",
                newName: "RegistrationDate");

            migrationBuilder.RenameColumn(
                name: "link_social",
                table: "Users",
                newName: "LinkSocial");

            migrationBuilder.RenameColumn(
                name: "link_github",
                table: "Users",
                newName: "LinkGithub");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Tags",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Tags",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Reputation",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "none");
        }
    }
}
