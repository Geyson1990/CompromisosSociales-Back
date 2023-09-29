using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addterritorialunitmanagers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TerritorialUnitId",
                table: "AppPersons",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPersons_TerritorialUnitId",
                table: "AppPersons",
                column: "TerritorialUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPersons_AppTerritorialUnits_TerritorialUnitId",
                table: "AppPersons",
                column: "TerritorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPersons_AppTerritorialUnits_TerritorialUnitId",
                table: "AppPersons");

            migrationBuilder.DropIndex(
                name: "IX_AppPersons_TerritorialUnitId",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "TerritorialUnitId",
                table: "AppPersons");
        }
    }
}
