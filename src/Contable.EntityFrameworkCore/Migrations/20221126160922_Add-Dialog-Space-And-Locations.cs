using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceAndLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDialogSpaces",
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
                    Code = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    Generation = table.Column<bool>(type: "BIT", nullable: false),
                    CaseName = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    SocialConflictId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaces_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppDialogSpaceLocations",
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
                    DialogSpaceId = table.Column<int>(type: "INT", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    RegionId = table.Column<int>(type: "INT", nullable: true),
                    Ubication = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppDialogSpaces_DialogSpaceId",
                        column: x => x.DialogSpaceId,
                        principalTable: "AppDialogSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "AppRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLocations_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_DepartmentId",
                table: "AppDialogSpaceLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_DialogSpaceId",
                table: "AppDialogSpaceLocations",
                column: "DialogSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_DistrictId",
                table: "AppDialogSpaceLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_ProvinceId",
                table: "AppDialogSpaceLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_RegionId",
                table: "AppDialogSpaceLocations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLocations_TerritorialUnitId",
                table: "AppDialogSpaceLocations",
                column: "TerritorialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaces_SocialConflictId",
                table: "AppDialogSpaces",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDialogSpaceLocations");

            migrationBuilder.DropTable(
                name: "AppDialogSpaces");
        }
    }
}
