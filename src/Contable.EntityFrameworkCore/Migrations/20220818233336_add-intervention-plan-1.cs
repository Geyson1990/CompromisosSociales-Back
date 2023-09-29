using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addinterventionplan1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInterventionPlanOptions",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    Index = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlans",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: true),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: true),
                    Code = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    Generation = table.Column<bool>(type: "BIT", nullable: false),
                    InterventionPlanTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CaseName = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Site = table.Column<int>(type: "INT", nullable: false),
                    PersonId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlans_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlans_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlans_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanActors",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Document = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Job = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Community = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    EmailAddress = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IsPoliticalAssociation = table.Column<bool>(type: "BIT", nullable: false),
                    PoliticalAssociation = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Position = table.Column<string>(type: "VARCHAR(3000)", nullable: true),
                    Interest = table.Column<string>(type: "VARCHAR(3000)", nullable: true),
                    Imported = table.Column<bool>(type: "BIT", nullable: false),
                    ActorTypeId = table.Column<int>(type: "INT", nullable: false),
                    ActorMovementId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanActors_AppActorMovements_ActorMovementId",
                        column: x => x.ActorMovementId,
                        principalTable: "AppActorMovements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanActors_AppActorTypes_ActorTypeId",
                        column: x => x.ActorTypeId,
                        principalTable: "AppActorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanActors_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanLocations",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    RegionId = table.Column<int>(type: "INT", nullable: true),
                    Ubication = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "AppRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanLocations_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanObjectives",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanObjectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanObjectives_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanRisks",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    RiskId = table.Column<int>(type: "INT", nullable: false),
                    RiskTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanRisks_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanRisks_AppRisks_RiskId",
                        column: x => x.RiskId,
                        principalTable: "AppRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanSolutions",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSolutions_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanStates",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanStates_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppInteventionPlanMethodologies",
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
                    InterventionPlanId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Methodology = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    InterventionPlanOptionId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInteventionPlanMethodologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInteventionPlanMethodologies_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInteventionPlanMethodologies_AppInterventionPlanOptions_InterventionPlanOptionId",
                        column: x => x.InterventionPlanOptionId,
                        principalTable: "AppInterventionPlanOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanActors_ActorMovementId",
                table: "AppInterventionPlanActors",
                column: "ActorMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanActors_ActorTypeId",
                table: "AppInterventionPlanActors",
                column: "ActorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanActors_InterventionPlanId",
                table: "AppInterventionPlanActors",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_DepartmentId",
                table: "AppInterventionPlanLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_DistrictId",
                table: "AppInterventionPlanLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_InterventionPlanId",
                table: "AppInterventionPlanLocations",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_ProvinceId",
                table: "AppInterventionPlanLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_RegionId",
                table: "AppInterventionPlanLocations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanLocations_TerritorialUnitId",
                table: "AppInterventionPlanLocations",
                column: "TerritorialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanObjectives_InterventionPlanId",
                table: "AppInterventionPlanObjectives",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanRisks_InterventionPlanId",
                table: "AppInterventionPlanRisks",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanRisks_RiskId",
                table: "AppInterventionPlanRisks",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlans_PersonId",
                table: "AppInterventionPlans",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlans_SocialConflictId",
                table: "AppInterventionPlans",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlans_SocialConflictSensibleId",
                table: "AppInterventionPlans",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSolutions_InterventionPlanId",
                table: "AppInterventionPlanSolutions",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanStates_InterventionPlanId",
                table: "AppInterventionPlanStates",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInteventionPlanMethodologies_InterventionPlanId",
                table: "AppInteventionPlanMethodologies",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInteventionPlanMethodologies_InterventionPlanOptionId",
                table: "AppInteventionPlanMethodologies",
                column: "InterventionPlanOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInterventionPlanActors");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanLocations");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanObjectives");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanRisks");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanSolutions");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanStates");

            migrationBuilder.DropTable(
                name: "AppInteventionPlanMethodologies");

            migrationBuilder.DropTable(
                name: "AppInterventionPlans");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanOptions");
        }
    }
}
