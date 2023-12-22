using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuestionsConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Questions",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Questions",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Questions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Questions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Questions",
                newName: "publication_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Questions",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "IdAuthor",
                table: "Questions",
                newName: "author_id");

            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "Questions",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publication_date",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_date",
                table: "Questions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Questions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Questions",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Questions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Questions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "publication_date",
                table: "Questions",
                newName: "PublicationDate");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "Questions",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Questions",
                newName: "IdAuthor");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Questions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "rating",
                table: "Answers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
