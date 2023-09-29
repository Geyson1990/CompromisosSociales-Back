using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprojectriskhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectRiskHistories",
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
                    ProjectRiskId = table.Column<int>(type: "INT", nullable: false),
                    StageId = table.Column<int>(type: "INT", nullable: true),
                    EvaluatedTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Total = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    FixProbabilityRate = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Probability = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    ProbabilityWeight = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Impact = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    FixImpactRate = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    ImpactWeight = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectRiskHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistories_AppProjectRisks_ProjectRiskId",
                        column: x => x.ProjectRiskId,
                        principalTable: "AppProjectRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistories_AppProjectStages_StageId",
                        column: x => x.StageId,
                        principalTable: "AppProjectStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectRiskHistoryDetails",
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
                    ProjectRiskHistoryId = table.Column<int>(type: "INT", nullable: false),
                    ProjectStageDetailId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableOptionId = table.Column<int>(type: "INT", nullable: false),
                    Weight = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectRiskHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistoryDetails_AppProjectRiskHistories_ProjectRiskHistoryId",
                        column: x => x.ProjectRiskHistoryId,
                        principalTable: "AppProjectRiskHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistoryDetails_AppProjectStageDetails_ProjectStageDetailId",
                        column: x => x.ProjectStageDetailId,
                        principalTable: "AppProjectStageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistoryDetails_AppStaticVariables_StaticVariableId",
                        column: x => x.StaticVariableId,
                        principalTable: "AppStaticVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskHistoryDetails_AppStaticVariableOptions_StaticVariableOptionId",
                        column: x => x.StaticVariableOptionId,
                        principalTable: "AppStaticVariableOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistories_ProjectRiskId",
                table: "AppProjectRiskHistories",
                column: "ProjectRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistories_StageId",
                table: "AppProjectRiskHistories",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistoryDetails_ProjectStageDetailId",
                table: "AppProjectRiskHistoryDetails",
                column: "ProjectStageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistoryDetails_StaticVariableId",
                table: "AppProjectRiskHistoryDetails",
                column: "StaticVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistoryDetails_StaticVariableOptionId",
                table: "AppProjectRiskHistoryDetails",
                column: "StaticVariableOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskHistoryDetails_ProjectRiskHistoryId_ProjectStageDetailId_StaticVariableOptionId",
                table: "AppProjectRiskHistoryDetails",
                columns: new[] { "ProjectRiskHistoryId", "ProjectStageDetailId", "StaticVariableOptionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectRiskHistoryDetails");

            migrationBuilder.DropTable(
                name: "AppProjectRiskHistories");
        }
    }
}
