using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertgenerationcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Generation",
                table: "AppSocialConflictAlerts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Generation",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AppSocialConflictAlerts");
        }
    }
}
