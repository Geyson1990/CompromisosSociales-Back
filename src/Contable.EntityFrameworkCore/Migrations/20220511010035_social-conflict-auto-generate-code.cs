using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class socialconflictautogeneratecode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Generation",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AppSocialConflicts");
        }
    }
}
