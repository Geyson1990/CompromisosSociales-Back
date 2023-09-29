using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlterCompromiseCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AppCompromises",
                type: "VARCHAR(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "AppCompromises",
                type: "VARCHAR(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldNullable: true);
        }
    }
}
