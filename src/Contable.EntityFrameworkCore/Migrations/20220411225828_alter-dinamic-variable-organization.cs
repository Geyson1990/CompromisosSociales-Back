using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterdinamicvariableorganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDinamicVariableDetails_AppDistricts_DistrictId",
                table: "AppDinamicVariableDetails");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "AppDinamicVariableDetails",
                newName: "ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId_DistrictId",
                table: "AppDinamicVariableDetails",
                newName: "IX_AppDinamicVariableDetails_DinamicVariableId_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDinamicVariableDetails_DistrictId",
                table: "AppDinamicVariableDetails",
                newName: "IX_AppDinamicVariableDetails_ProvinceId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "AppProspectiveRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "AppProspectiveRisks",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AppProspectiveRiskDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProspectiveRiskId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableOptionId = table.Column<int>(type: "INT", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProspectiveRiskDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskDetails_AppProspectiveRisks_ProspectiveRiskId",
                        column: x => x.ProspectiveRiskId,
                        principalTable: "AppProspectiveRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskDetails_AppStaticVariableOptions_StaticVariableOptionId",
                        column: x => x.StaticVariableOptionId,
                        principalTable: "AppStaticVariableOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskDetails_StaticVariableOptionId",
                table: "AppProspectiveRiskDetails",
                column: "StaticVariableOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskDetails_ProspectiveRiskId_StaticVariableOptionId",
                table: "AppProspectiveRiskDetails",
                columns: new[] { "ProspectiveRiskId", "StaticVariableOptionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppDinamicVariableDetails_AppProvinces_ProvinceId",
                table: "AppDinamicVariableDetails",
                column: "ProvinceId",
                principalTable: "AppProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDinamicVariableDetails_AppProvinces_ProvinceId",
                table: "AppDinamicVariableDetails");

            migrationBuilder.DropTable(
                name: "AppProspectiveRiskDetails");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "AppDinamicVariableDetails",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId_ProvinceId",
                table: "AppDinamicVariableDetails",
                newName: "IX_AppDinamicVariableDetails_DinamicVariableId_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDinamicVariableDetails_ProvinceId",
                table: "AppDinamicVariableDetails",
                newName: "IX_AppDinamicVariableDetails_DistrictId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "AppProspectiveRisks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(27,2)");

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "AppProspectiveRisks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDinamicVariableDetails_AppDistricts_DistrictId",
                table: "AppDinamicVariableDetails",
                column: "DistrictId",
                principalTable: "AppDistricts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
