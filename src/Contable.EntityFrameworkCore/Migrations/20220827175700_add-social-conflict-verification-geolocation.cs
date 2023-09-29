using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictverificationgeolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(10,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "AppSocialConflicts",
                type: "DECIMAL(10,10)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "AppSocialConflicts");
        }
    }
}
