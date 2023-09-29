using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlerts",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    AlterTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlerts_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlerts_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlertLocations",
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
                    SocialConflictAlertId = table.Column<int>(type: "INT", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    Region = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Ubication = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlertLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertLocations_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertLocations_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_DepartmentId",
                table: "AppSocialConflictAlertLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_DistrictId",
                table: "AppSocialConflictAlertLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_ProvinceId",
                table: "AppSocialConflictAlertLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_SocialConflictAlertId",
                table: "AppSocialConflictAlertLocations",
                column: "SocialConflictAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertLocations_TerritorialUnitId",
                table: "AppSocialConflictAlertLocations",
                column: "TerritorialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_SocialConflictId",
                table: "AppSocialConflictAlerts",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_TerritorialUnitId",
                table: "AppSocialConflictAlerts",
                column: "TerritorialUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictAlertLocations");

            migrationBuilder.DropTable(
                name: "AppSocialConflictAlerts");
        }
    }
}
