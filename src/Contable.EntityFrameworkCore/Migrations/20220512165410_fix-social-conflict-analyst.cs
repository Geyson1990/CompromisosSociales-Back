using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixsocialconflictanalyst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerrotorialUnitId",
                table: "AppAnalystTerritorialUnits");

            migrationBuilder.RenameColumn(
                name: "TerrotorialUnitId",
                table: "AppAnalystTerritorialUnits",
                newName: "TerritorialUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AppAnalystTerritorialUnits_TerrotorialUnitId",
                table: "AppAnalystTerritorialUnits",
                newName: "IX_AppAnalystTerritorialUnits_TerritorialUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerritorialUnitId",
                table: "AppAnalystTerritorialUnits",
                column: "TerritorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerritorialUnitId",
                table: "AppAnalystTerritorialUnits");

            migrationBuilder.RenameColumn(
                name: "TerritorialUnitId",
                table: "AppAnalystTerritorialUnits",
                newName: "TerrotorialUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AppAnalystTerritorialUnits_TerritorialUnitId",
                table: "AppAnalystTerritorialUnits",
                newName: "IX_AppAnalystTerritorialUnits_TerrotorialUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerrotorialUnitId",
                table: "AppAnalystTerritorialUnits",
                column: "TerrotorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
