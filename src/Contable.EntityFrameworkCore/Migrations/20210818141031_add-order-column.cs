using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addordercolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CaseName",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSituations",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AppParameter",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCompromises",
                type: "VARCHAR(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "AppParameter");

            migrationBuilder.AlterColumn<string>(
                name: "CaseName",
                table: "AppSocialConflicts",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSituations",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppCompromises",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)");
        }
    }
}
