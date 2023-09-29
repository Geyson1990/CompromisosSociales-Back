using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictViolenceFacts",
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
                    ManagerId = table.Column<int>(type: "INT", nullable: false),
                    FactId = table.Column<int>(type: "INT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EndTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Responsible = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    InjuredMen = table.Column<int>(type: "INT", nullable: false),
                    InjuredWomen = table.Column<int>(type: "INT", nullable: false),
                    DeadMen = table.Column<int>(type: "INT", nullable: false),
                    DeadWomen = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictViolenceFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFacts_AppFacts_FactId",
                        column: x => x.FactId,
                        principalTable: "AppFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFacts_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFacts_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictViolenceFactLocations",
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
                    SocialConflictViolenceFactId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictViolenceFactLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFactLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFactLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFactLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictViolenceFactLocations_AppSocialConflictViolenceFacts_SocialConflictViolenceFactId",
                        column: x => x.SocialConflictViolenceFactId,
                        principalTable: "AppSocialConflictViolenceFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_DepartmentId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_DistrictId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_ProvinceId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFactLocations_SocialConflictViolenceFactId",
                table: "AppSocialConflictViolenceFactLocations",
                column: "SocialConflictViolenceFactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFacts_FactId",
                table: "AppSocialConflictViolenceFacts",
                column: "FactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFacts_ManagerId",
                table: "AppSocialConflictViolenceFacts",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictViolenceFacts_SocialConflictId",
                table: "AppSocialConflictViolenceFacts",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropTable(
                name: "AppSocialConflictViolenceFacts");
        }
    }
}
