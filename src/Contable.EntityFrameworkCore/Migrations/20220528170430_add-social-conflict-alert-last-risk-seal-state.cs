using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertlastrisksealstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastAlertRiskId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastSealId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAlertRiskId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "LastSealId",
                table: "AppSocialConflictAlerts");
        }
    }
}
