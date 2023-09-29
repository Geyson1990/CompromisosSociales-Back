using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addinterventionplanschedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInterventionPlanActivities",
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
                    table.PrimaryKey("PK_AppInterventionPlanActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanEntities",
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
                    Type = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanSchedules",
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
                    Schedule = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    ScheduleTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    InterventionPlanActivityId = table.Column<int>(type: "INT", nullable: false),
                    Activity = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    InterventionPlanEntityId = table.Column<int>(type: "INT", nullable: false),
                    AlertResponsibleId = table.Column<int>(type: "INT", nullable: true),
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: true),
                    Entity = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSchedules_AppAlertResponsibles_AlertResponsibleId",
                        column: x => x.AlertResponsibleId,
                        principalTable: "AppAlertResponsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSchedules_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSchedules_AppInterventionPlanActivities_InterventionPlanActivityId",
                        column: x => x.InterventionPlanActivityId,
                        principalTable: "AppInterventionPlanActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSchedules_AppInterventionPlanEntities_InterventionPlanEntityId",
                        column: x => x.InterventionPlanEntityId,
                        principalTable: "AppInterventionPlanEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanSchedules_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_AlertResponsibleId",
                table: "AppInterventionPlanSchedules",
                column: "AlertResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_DirectoryGovernmentId",
                table: "AppInterventionPlanSchedules",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_InterventionPlanActivityId",
                table: "AppInterventionPlanSchedules",
                column: "InterventionPlanActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_InterventionPlanEntityId",
                table: "AppInterventionPlanSchedules",
                column: "InterventionPlanEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_InterventionPlanId",
                table: "AppInterventionPlanSchedules",
                column: "InterventionPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInterventionPlanSchedules");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanActivities");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanEntities");
        }
    }
}
