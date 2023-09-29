using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterinterventionplanpersonoptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInterventionPlans_AppPersons_PersonId",
                table: "AppInterventionPlans");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "AppInterventionPlans",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInterventionPlans_AppPersons_PersonId",
                table: "AppInterventionPlans",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInterventionPlans_AppPersons_PersonId",
                table: "AppInterventionPlans");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "AppInterventionPlans",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInterventionPlans_AppPersons_PersonId",
                table: "AppInterventionPlans",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
