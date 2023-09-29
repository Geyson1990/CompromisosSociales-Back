using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterprojectriskhistorystagerequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRiskHistories_AppProjectStages_StageId",
                table: "AppProjectRiskHistories");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "AppProjectRiskHistories",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRiskHistories_AppProjectStages_StageId",
                table: "AppProjectRiskHistories",
                column: "StageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRiskHistories_AppProjectStages_StageId",
                table: "AppProjectRiskHistories");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "AppProjectRiskHistories",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRiskHistories_AppProjectStages_StageId",
                table: "AppProjectRiskHistories",
                column: "StageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
