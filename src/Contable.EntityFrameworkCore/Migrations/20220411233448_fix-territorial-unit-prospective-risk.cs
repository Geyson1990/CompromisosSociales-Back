using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixterritorialunitprospectiverisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProspectiveRisks_AppDistricts_DistrictId",
                table: "AppProspectiveRisks");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "AppProspectiveRisks",
                newName: "ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProspectiveRisks_DistrictId",
                table: "AppProspectiveRisks",
                newName: "IX_AppProspectiveRisks_ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProspectiveRisks_AppProvinces_ProvinceId",
                table: "AppProspectiveRisks",
                column: "ProvinceId",
                principalTable: "AppProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProspectiveRisks_AppProvinces_ProvinceId",
                table: "AppProspectiveRisks");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "AppProspectiveRisks",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProspectiveRisks_ProvinceId",
                table: "AppProspectiveRisks",
                newName: "IX_AppProspectiveRisks_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProspectiveRisks_AppDistricts_DistrictId",
                table: "AppProspectiveRisks",
                column: "DistrictId",
                principalTable: "AppDistricts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
