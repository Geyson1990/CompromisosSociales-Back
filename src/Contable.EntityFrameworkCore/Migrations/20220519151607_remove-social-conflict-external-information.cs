using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class removesocialconflictexternalinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalCaseName",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ExternalCode",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "AppSocialConflicts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }
    }
}
