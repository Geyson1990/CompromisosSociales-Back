using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcompromisephasemilestones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppParameterCategory",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AppParameter",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppParameterCategory",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "AppParameter",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);
        }
    }
}
