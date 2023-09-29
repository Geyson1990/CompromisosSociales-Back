using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addnamesurnameperson2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppPersons",
                type: "VARCHAR(765)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname2",
                table: "AppPersons",
                type: "VARCHAR(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname2",
                table: "AppPersons");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppPersons",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(765)",
                oldNullable: true);
        }
    }
}
