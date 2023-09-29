using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addregionsocialconflictlocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AppSocialConflictLocations");

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AppSocialConflictViolenceFactLocations",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "AppSocialConflictLocations",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_RegionId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictLocations_RegionId",
                table: "AppSocialConflictLocations",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictLocations_AppRegions_RegionId",
                table: "AppSocialConflictLocations",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictViolenceFactLocations_AppRegions_RegionId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "RegionId",
                principalTable: "AppRegions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictLocations_AppRegions_RegionId",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictViolenceFactLocations_AppRegions_RegionId",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_RegionId",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictLocations_RegionId",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "AppSocialConflictLocations");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AppSocialConflictViolenceFactLocations",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AppSocialConflictLocations",
                type: "VARCHAR(255)",
                nullable: true);
        }
    }
}
