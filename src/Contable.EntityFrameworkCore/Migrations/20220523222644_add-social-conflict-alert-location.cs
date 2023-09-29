using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertlocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "AppSocialConflictAlertLocations");

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AppSocialConflictAlertLocations",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_RegionId",
                table: "AppSocialConflictAlertLocations",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlertLocations_AppRegions_RegionId",
                table: "AppSocialConflictAlertLocations",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlertLocations_AppRegions_RegionId",
                table: "AppSocialConflictAlertLocations");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlertLocations_RegionId",
                table: "AppSocialConflictAlertLocations");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AppSocialConflictAlertLocations");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AppSocialConflictAlertLocations",
                type: "VARCHAR(255)",
                nullable: true);
        }
    }
}
