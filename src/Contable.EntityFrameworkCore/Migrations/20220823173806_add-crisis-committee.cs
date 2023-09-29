using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcrisiscommittee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeJobs",
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
                    ShowDescription = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommittees",
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
                    Code = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    Generation = table.Column<bool>(type: "BIT", nullable: false),
                    CrisisComitteTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CaseName = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommittees_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeTeams",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    AlertResponsibleId = table.Column<int>(type: "INT", nullable: false),
                    CrisisCommitteeJobId = table.Column<int>(type: "INT", nullable: false),
                    Job = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Document = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Surname = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SecondSurname = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeTeams_AppAlertResponsibles_AlertResponsibleId",
                        column: x => x.AlertResponsibleId,
                        principalTable: "AppAlertResponsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeTeams_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeTeams_AppCrisisCommitteeJobs_CrisisCommitteeJobId",
                        column: x => x.CrisisCommitteeJobId,
                        principalTable: "AppCrisisCommitteeJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommittees_InterventionPlanId",
                table: "AppCrisisCommittees",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeTeams_AlertResponsibleId",
                table: "AppCrisisCommitteeTeams",
                column: "AlertResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeTeams_CrisisCommitteeId",
                table: "AppCrisisCommitteeTeams",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeTeams_CrisisCommitteeJobId",
                table: "AppCrisisCommitteeTeams",
                column: "CrisisCommitteeJobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeTeams");

            migrationBuilder.DropTable(
                name: "AppCrisisCommittees");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeJobs");
        }
    }
}
