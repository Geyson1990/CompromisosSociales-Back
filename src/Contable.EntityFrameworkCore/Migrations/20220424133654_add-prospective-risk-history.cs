using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprospectiveriskhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EvaluatedTime",
                table: "AppProspectiveRisks",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppProspectiveRiskHistories",
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
                    ProspectiveRiskId = table.Column<int>(type: "INT", nullable: false),
                    EvaluatedTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Weight = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    FixValue = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProspectiveRiskHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskHistories_AppProspectiveRisks_ProspectiveRiskId",
                        column: x => x.ProspectiveRiskId,
                        principalTable: "AppProspectiveRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AppProspectiveRiskHistoryDetails",
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
                    ProspectiveRiskHistoryId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableOptionId = table.Column<int>(type: "INT", nullable: false),
                    Weight = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProspectiveRiskHistoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskHistoryDetails_AppProspectiveRiskHistories_ProspectiveRiskHistoryId",
                        column: x => x.ProspectiveRiskHistoryId,
                        principalTable: "AppProspectiveRiskHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskHistoryDetails_AppStaticVariables_StaticVariableId",
                        column: x => x.StaticVariableId,
                        principalTable: "AppStaticVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppProspectiveRiskHistoryDetails_AppStaticVariableOptions_StaticVariableOptionId",
                        column: x => x.StaticVariableOptionId,
                        principalTable: "AppStaticVariableOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskHistories_ProspectiveRiskId",
                table: "AppProspectiveRiskHistories",
                column: "ProspectiveRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskHistoryDetails_StaticVariableId",
                table: "AppProspectiveRiskHistoryDetails",
                column: "StaticVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskHistoryDetails_StaticVariableOptionId",
                table: "AppProspectiveRiskHistoryDetails",
                column: "StaticVariableOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProspectiveRiskHistoryDetails_ProspectiveRiskHistoryId_StaticVariableOptionId",
                table: "AppProspectiveRiskHistoryDetails",
                columns: new[] { "ProspectiveRiskHistoryId", "StaticVariableOptionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProspectiveRiskHistoryDetails");

            migrationBuilder.DropTable(
                name: "AppProspectiveRiskHistories");

            migrationBuilder.DropColumn(
                name: "EvaluatedTime",
                table: "AppProspectiveRisks");
        }
    }
}
