using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeAnswers.Data.Migrations
{
    /// <inheritdoc />
    public partial class answconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Answers",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Answers",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Answers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Answers",
                newName: "publication_date");

            migrationBuilder.RenameColumn(
                name: "IdQuestion",
                table: "Answers",
                newName: "question_id");

            migrationBuilder.RenameColumn(
                name: "IdAuthor",
                table: "Answers",
                newName: "author_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publication_date",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Answers",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Answers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Answers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "publication_date",
                table: "Answers",
                newName: "PublicationDate");

            migrationBuilder.RenameColumn(
                name: "question_id",
                table: "Answers",
                newName: "IdQuestion");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Answers",
                newName: "IdAuthor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublicationDate",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
