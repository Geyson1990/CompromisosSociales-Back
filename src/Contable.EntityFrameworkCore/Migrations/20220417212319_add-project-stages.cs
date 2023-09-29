using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprojectstages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppProjectStages",
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
                    Name = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppProjectStageDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectStageId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProjectStageDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProjectStageDetails_AppProjectStages_ProjectStageId",
                        column: x => x.ProjectStageId,
                        principalTable: "AppProjectStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppProjectStageDetails_AppStaticVariables_StaticVariableId",
                        column: x => x.StaticVariableId,
                        principalTable: "AppStaticVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectStageDetails_StaticVariableId",
                table: "AppProjectStageDetails",
                column: "StaticVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectStageDetails_ProjectStageId_StaticVariableId",
                table: "AppProjectStageDetails",
                columns: new[] { "ProjectStageId", "StaticVariableId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProjectStageDetails");

            migrationBuilder.DropTable(
                name: "AppProjectStages");
        }
    }
}
