using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddinamicvariablerestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId",
                table: "AppDinamicVariableDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId_DistrictId",
                table: "AppDinamicVariableDetails",
                columns: new[] { "DinamicVariableId", "DistrictId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId_DistrictId",
                table: "AppDinamicVariableDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId",
                table: "AppDinamicVariableDetails",
                column: "DinamicVariableId");
        }
    }
}
