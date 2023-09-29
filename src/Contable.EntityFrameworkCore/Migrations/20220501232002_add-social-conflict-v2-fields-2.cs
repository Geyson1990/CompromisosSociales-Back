using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AppSocialConflictLocations",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubication",
                table: "AppSocialConflictLocations",
                type: "VARCHAR(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "Ubication",
                table: "AppSocialConflictLocations");
        }
    }
}
