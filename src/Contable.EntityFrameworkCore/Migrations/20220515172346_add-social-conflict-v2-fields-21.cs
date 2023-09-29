using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GovernmentLevel",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TerritorialUnitId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_TerritorialUnitId",
                table: "AppSocialConflicts",
                column: "TerritorialUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppTerritorialUnits_TerritorialUnitId",
                table: "AppSocialConflicts",
                column: "TerritorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppTerritorialUnits_TerritorialUnitId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_TerritorialUnitId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "GovernmentLevel",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "TerritorialUnitId",
                table: "AppSocialConflicts");
        }
    }
}
