using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddInterventionPlanScheduleMethodologyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppInterventionPlanSchedules_InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules",
                column: "InterventionPlanMethodologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInterventionPlanSchedules_AppInteventionPlanMethodologies_InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules",
                column: "InterventionPlanMethodologyId",
                principalTable: "AppInteventionPlanMethodologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInterventionPlanSchedules_AppInteventionPlanMethodologies_InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules");

            migrationBuilder.DropIndex(
                name: "IX_AppInterventionPlanSchedules_InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules");

            migrationBuilder.DropColumn(
                name: "InterventionPlanMethodologyId",
                table: "AppInterventionPlanSchedules");
        }
    }
}
