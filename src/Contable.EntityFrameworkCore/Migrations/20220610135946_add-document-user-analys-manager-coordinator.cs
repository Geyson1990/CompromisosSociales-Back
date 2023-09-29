using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddocumentuseranalysmanagercoordinator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "AppPersons",
                type: "VARCHAR(25)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "AbpUsers",
                type: "VARCHAR(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Document",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "AbpUsers");
        }
    }
}
