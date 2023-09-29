using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addnamesurnameperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppPersons",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Names",
                table: "AppPersons",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AppPersons",
                type: "VARCHAR(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Names",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AppPersons");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppPersons",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);
        }
    }
}
