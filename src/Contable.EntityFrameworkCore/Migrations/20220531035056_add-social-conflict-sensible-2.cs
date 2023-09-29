using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsensible2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictSensibles_AppSectors_SectorId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictSensibles_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleViolenceFactLocations");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleViolenceFacts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictSensibles_SectorId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictSensibles_SubTypologyId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Dialog",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "FactorContext",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "GovernmentLevel",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Plaint",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "SubTypologyId",
                table: "AppSocialConflictSensibles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppSocialConflictSensibles",
                type: "VARCHAR(5000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dialog",
                table: "AppSocialConflictSensibles",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FactorContext",
                table: "AppSocialConflictSensibles",
                type: "VARCHAR(5000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GovernmentLevel",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Plaint",
                table: "AppSocialConflictSensibles",
                type: "VARCHAR(6000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strategy",
                table: "AppSocialConflictSensibles",
                type: "VARCHAR(5000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubTypologyId",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleViolenceFacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Actions = table.Column<string>(type: "VARCHAR(2000)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeadMen = table.Column<int>(type: "INT", nullable: false),
                    DeadWomen = table.Column<int>(type: "INT", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    FactId = table.Column<int>(type: "INT", nullable: false),
                    InjuredMen = table.Column<int>(type: "INT", nullable: false),
                    InjuredWomen = table.Column<int>(type: "INT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerId = table.Column<int>(type: "INT", nullable: true),
                    Responsible = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleViolenceFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFacts_AppFacts_FactId",
                        column: x => x.FactId,
                        principalTable: "AppFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFacts_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFacts_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleViolenceFactLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    RegionId = table.Column<int>(type: "INT", nullable: true),
                    SocialConflictSensibleViolenceFactId = table.Column<int>(type: "INT", nullable: false),
                    Ubication = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleViolenceFactLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFactLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFactLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFactLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFactLocations_AppRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "AppRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleViolenceFactLocations_AppSocialConflictSensibleViolenceFacts_SocialConflictSensibleViolenceFactId",
                        column: x => x.SocialConflictSensibleViolenceFactId,
                        principalTable: "AppSocialConflictSensibleViolenceFacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_SectorId",
                table: "AppSocialConflictSensibles",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_SubTypologyId",
                table: "AppSocialConflictSensibles",
                column: "SubTypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFactLocations_DepartmentId",
                table: "AppSocialConflictSensibleViolenceFactLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFactLocations_DistrictId",
                table: "AppSocialConflictSensibleViolenceFactLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFactLocations_ProvinceId",
                table: "AppSocialConflictSensibleViolenceFactLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFactLocations_RegionId",
                table: "AppSocialConflictSensibleViolenceFactLocations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFactLocations_SocialConflictSensibleViolenceFactId",
                table: "AppSocialConflictSensibleViolenceFactLocations",
                column: "SocialConflictSensibleViolenceFactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFacts_FactId",
                table: "AppSocialConflictSensibleViolenceFacts",
                column: "FactId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFacts_ManagerId",
                table: "AppSocialConflictSensibleViolenceFacts",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleViolenceFacts_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleViolenceFacts",
                column: "SocialConflictSensibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictSensibles_AppSectors_SectorId",
                table: "AppSocialConflictSensibles",
                column: "SectorId",
                principalTable: "AppSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictSensibles_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflictSensibles",
                column: "SubTypologyId",
                principalTable: "AppSubTypologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
