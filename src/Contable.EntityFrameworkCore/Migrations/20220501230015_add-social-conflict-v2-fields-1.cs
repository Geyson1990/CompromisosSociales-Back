using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalCaseName",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalCode",
                table: "AppSocialConflicts",
                type: "VARCHAR(20)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GeographicType",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalCaseName",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ExternalCode",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "GeographicType",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "AppSocialConflicts");
        }
    }
}
