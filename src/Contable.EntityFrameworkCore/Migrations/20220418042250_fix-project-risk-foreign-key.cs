using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixprojectriskforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_ProjectStageId",
                table: "AppProjectRisks");

            migrationBuilder.RenameColumn(
                name: "ProjectStageId",
                table: "AppProjectRisks",
                newName: "StageId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProjectRisks_ProjectStageId",
                table: "AppProjectRisks",
                newName: "IX_AppProjectRisks_StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks",
                column: "StageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks");

            migrationBuilder.RenameColumn(
                name: "StageId",
                table: "AppProjectRisks",
                newName: "ProjectStageId");

            migrationBuilder.RenameIndex(
                name: "IX_AppProjectRisks_StageId",
                table: "AppProjectRisks",
                newName: "IX_AppProjectRisks_ProjectStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_ProjectStageId",
                table: "AppProjectRisks",
                column: "ProjectStageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
