using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprojectriskalternativestagerelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "AppProjectRisks",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks",
                column: "StageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks");

            migrationBuilder.AlterColumn<int>(
                name: "StageId",
                table: "AppProjectRisks",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppProjectRisks_AppProjectStages_StageId",
                table: "AppProjectRisks",
                column: "StageId",
                principalTable: "AppProjectStages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
