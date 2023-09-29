using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprojectrisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectRisks",
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
                    ProvinceId = table.Column<int>(type: "INT", nullable: false),
                    ProjectStageId = table.Column<int>(type: "INT", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    EvaluatedTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Total = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectRisks_AppProjectStages_ProjectStageId",
                        column: x => x.ProjectStageId,
                        principalTable: "AppProjectStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRisks_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectRiskDetails",
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
                    ProjectStageDetailId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableOptionId = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectRiskDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskDetails_AppProjectRisks_ProjectRiskId",
                        column: x => x.ProjectRiskId,
                        principalTable: "AppProjectRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskDetails_AppProjectStageDetails_ProjectStageDetailId",
                        column: x => x.ProjectStageDetailId,
                        principalTable: "AppProjectStageDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProjectRiskDetails_AppStaticVariableOptions_StaticVariableOptionId",
                        column: x => x.StaticVariableOptionId,
                        principalTable: "AppStaticVariableOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskDetails_ProjectStageDetailId",
                table: "AppProjectRiskDetails",
                column: "ProjectStageDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskDetails_StaticVariableOptionId",
                table: "AppProjectRiskDetails",
                column: "StaticVariableOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRiskDetails_ProjectRiskId_ProjectStageDetailId_StaticVariableOptionId",
                table: "AppProjectRiskDetails",
                columns: new[] { "ProjectRiskId", "ProjectStageDetailId", "StaticVariableOptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRisks_ProjectStageId",
                table: "AppProjectRisks",
                column: "ProjectStageId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectRisks_ProvinceId",
                table: "AppProjectRisks",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectRiskDetails");

            migrationBuilder.DropTable(
                name: "AppProjectRisks");
        }
    }
}
