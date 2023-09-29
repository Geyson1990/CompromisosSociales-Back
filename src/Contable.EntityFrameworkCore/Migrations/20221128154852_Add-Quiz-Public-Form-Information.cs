using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddQuizPublicFormInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AppQuizCompletes",
                type: "VARCHAR(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AppQuizCompletes",
                type: "VARCHAR(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondSurname",
                table: "AppQuizCompletes",
                type: "VARCHAR(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AppQuizCompletes",
                type: "VARCHAR(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AppQuizCompletes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AppQuizCompletes");

            migrationBuilder.DropColumn(
                name: "SecondSurname",
                table: "AppQuizCompletes");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AppQuizCompletes");
        }
    }
}
