using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addinterventionplanteam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInterventionPlanRoles",
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
                    table.PrimaryKey("PK_AppInterventionPlanRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInterventionPlanTeams",
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
                    Document = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Surname = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SecondSurname = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    InterventionPlanEntityId = table.Column<int>(type: "INT", nullable: false),
                    AlertResponsibleId = table.Column<int>(type: "INT", nullable: true),
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: true),
                    Entity = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Job = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    InterventionPlanRoleId = table.Column<int>(type: "INT", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInterventionPlanTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanTeams_AppAlertResponsibles_AlertResponsibleId",
                        column: x => x.AlertResponsibleId,
                        principalTable: "AppAlertResponsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanTeams_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanTeams_AppInterventionPlanEntities_InterventionPlanEntityId",
                        column: x => x.InterventionPlanEntityId,
                        principalTable: "AppInterventionPlanEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanTeams_AppInterventionPlans_InterventionPlanId",
                        column: x => x.InterventionPlanId,
                        principalTable: "AppInterventionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppInterventionPlanTeams_AppInterventionPlanRoles_InterventionPlanRoleId",
                        column: x => x.InterventionPlanRoleId,
                        principalTable: "AppInterventionPlanRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanTeams_AlertResponsibleId",
                table: "AppInterventionPlanTeams",
                column: "AlertResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanTeams_DirectoryGovernmentId",
                table: "AppInterventionPlanTeams",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanTeams_InterventionPlanEntityId",
                table: "AppInterventionPlanTeams",
                column: "InterventionPlanEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanTeams_InterventionPlanId",
                table: "AppInterventionPlanTeams",
                column: "InterventionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanTeams_InterventionPlanRoleId",
                table: "AppInterventionPlanTeams",
                column: "InterventionPlanRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInterventionPlanTeams");

            migrationBuilder.DropTable(
                name: "AppInterventionPlanRoles");
        }
    }
}
