using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictgeneralinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AlterTime",
                table: "AppSocialConflictAlerts",
                newName: "AlertTime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "AppSocialConflictAlerts");

            migrationBuilder.RenameColumn(
                name: "AlertTime",
                table: "AppSocialConflictAlerts",
                newName: "AlterTime");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);
        }
    }
}
