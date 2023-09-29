using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsensible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SocialConflictSensibleId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppDinamicVariables",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibles",
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
                    CaseName = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Problem = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Dialog = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Plaint = table.Column<string>(type: "VARCHAR(6000)", nullable: true),
                    FactorContext = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Strategy = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    GeographicType = table.Column<int>(type: "INT", nullable: false),
                    GovernmentLevel = table.Column<int>(type: "INT", nullable: false),
                    AnalystId = table.Column<int>(type: "INT", nullable: true),
                    CoordinatorId = table.Column<int>(type: "INT", nullable: true),
                    ManagerId = table.Column<int>(type: "INT", nullable: true),
                    SectorId = table.Column<int>(type: "INT", nullable: true),
                    TypologyId = table.Column<int>(type: "INT", nullable: true),
                    SubTypologyId = table.Column<int>(type: "INT", nullable: true),
                    LastCondition = table.Column<int>(type: "INT", nullable: false),
                    Filter = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppPersons_AnalystId",
                        column: x => x.AnalystId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppPersons_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppSectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "AppSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppSubTypologies_SubTypologyId",
                        column: x => x.SubTypologyId,
                        principalTable: "AppSubTypologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibles_AppTypologies_TypologyId",
                        column: x => x.TypologyId,
                        principalTable: "AppTypologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleConditions",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    ConditionTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleConditions_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleGeneralFacts",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(6000)", nullable: true),
                    FactTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleGeneralFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleGeneralFacts_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    RegionId = table.Column<int>(type: "INT", nullable: true),
                    Ubication = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "AppRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleLocations_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleManagements",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    ManagementId = table.Column<int>(type: "INT", nullable: false),
                    ManagerId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    ManagementTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CivilMen = table.Column<int>(type: "INT", nullable: true),
                    CivilWomen = table.Column<int>(type: "INT", nullable: true),
                    StateMen = table.Column<int>(type: "INT", nullable: true),
                    StateWomen = table.Column<int>(type: "INT", nullable: true),
                    CompanyMen = table.Column<int>(type: "INT", nullable: true),
                    CompanyWomen = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleManagements_AppManagements_ManagementId",
                        column: x => x.ManagementId,
                        principalTable: "AppManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleManagements_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleManagements_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleRisks",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    RiskId = table.Column<int>(type: "INT", nullable: false),
                    RiskTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleRisks_AppRisks_RiskId",
                        column: x => x.RiskId,
                        principalTable: "AppRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleRisks_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleStates",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    ManagerId = table.Column<int>(type: "INT", nullable: false),
                    StateTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleStates_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleStates_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleSugerences",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Accepted = table.Column<bool>(type: "BIT", nullable: false),
                    AcceptTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    AcceptedUserId = table.Column<long>(type: "BIGINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleSugerences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleSugerences_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleViolenceFacts",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    ManagerId = table.Column<int>(type: "INT", nullable: true),
                    FactId = table.Column<int>(type: "INT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    EndTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(2000)", nullable: true),
                    Responsible = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Actions = table.Column<string>(type: "VARCHAR(2000)", nullable: true),
                    InjuredMen = table.Column<int>(type: "INT", nullable: false),
                    InjuredWomen = table.Column<int>(type: "INT", nullable: false),
                    DeadMen = table.Column<int>(type: "INT", nullable: false),
                    DeadWomen = table.Column<int>(type: "INT", nullable: false)
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
                name: "AppSocialConflictSensibleManagementResources",
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
                    SocialConflictSensibleManagementId = table.Column<int>(type: "INT", nullable: false),
                    CommonFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ResourceFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SectionFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    FileName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Size = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Extension = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ClassName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Resource = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleManagementResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleManagementResources_AppSocialConflictSensibleManagements_SocialConflictSensibleManagementId",
                        column: x => x.SocialConflictSensibleManagementId,
                        principalTable: "AppSocialConflictSensibleManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleViolenceFactLocations",
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
                    SocialConflictSensibleViolenceFactId = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: false),
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    RegionId = table.Column<int>(type: "INT", nullable: true),
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
                name: "IX_AppSocialConflictActors_SocialConflictSensibleId",
                table: "AppSocialConflictActors",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleConditions_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleConditions",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleGeneralFacts_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleGeneralFacts",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_DepartmentId",
                table: "AppSocialConflictSensibleLocations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_DistrictId",
                table: "AppSocialConflictSensibleLocations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_ProvinceId",
                table: "AppSocialConflictSensibleLocations",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_RegionId",
                table: "AppSocialConflictSensibleLocations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleLocations",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleLocations_TerritorialUnitId",
                table: "AppSocialConflictSensibleLocations",
                column: "TerritorialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleManagementResources_SocialConflictSensibleManagementId",
                table: "AppSocialConflictSensibleManagementResources",
                column: "SocialConflictSensibleManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleManagements_ManagementId",
                table: "AppSocialConflictSensibleManagements",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleManagements_ManagerId",
                table: "AppSocialConflictSensibleManagements",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleManagements_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleManagements",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleRisks_RiskId",
                table: "AppSocialConflictSensibleRisks",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleRisks_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleRisks",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_AnalystId",
                table: "AppSocialConflictSensibles",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_CoordinatorId",
                table: "AppSocialConflictSensibles",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_ManagerId",
                table: "AppSocialConflictSensibles",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_SectorId",
                table: "AppSocialConflictSensibles",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_SubTypologyId",
                table: "AppSocialConflictSensibles",
                column: "SubTypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibles_TypologyId",
                table: "AppSocialConflictSensibles",
                column: "TypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleStates_ManagerId",
                table: "AppSocialConflictSensibleStates",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleStates_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleStates",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleSugerences_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleSugerences",
                column: "SocialConflictSensibleId");

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
                name: "FK_AppSocialConflictActors_AppSocialConflictSensibles_SocialConflictSensibleId",
                table: "AppSocialConflictActors",
                column: "SocialConflictSensibleId",
                principalTable: "AppSocialConflictSensibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflictSensibles_SocialConflictSensibleId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleConditions");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleGeneralFacts");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleLocations");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleManagementResources");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleRisks");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleStates");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleSugerences");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleViolenceFactLocations");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleManagements");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleViolenceFacts");

            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibles");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictActors_SocialConflictSensibleId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "SocialConflictSensibleId",
                table: "AppSocialConflictActors");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppDinamicVariables",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);
        }
    }
}
