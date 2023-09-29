using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class altersocialconflictverificationgeolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "AppSocialConflictSensibles",
                type: "DECIMAL(27,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "AppSocialConflictSensibles",
                type: "DECIMAL(27,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(27,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(27,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,10)");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "AppSocialConflictAlerts",
                type: "DECIMAL(27,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "AppSocialConflictAlerts",
                type: "DECIMAL(27,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "AppSocialConflictAlerts",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "AppSocialConflictAlerts");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(10,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(27,10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(10,10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(27,10)");
        }
    }
}
