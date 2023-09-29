using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterstaticvariableoptionoptionaldinamicvariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppStaticVariableOptions_AppDinamicVariables_DinamicVariableId",
                table: "AppStaticVariableOptions");

            migrationBuilder.AlterColumn<int>(
                name: "DinamicVariableId",
                table: "AppStaticVariableOptions",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppStaticVariableOptions_AppDinamicVariables_DinamicVariableId",
                table: "AppStaticVariableOptions",
                column: "DinamicVariableId",
                principalTable: "AppDinamicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppStaticVariableOptions_AppDinamicVariables_DinamicVariableId",
                table: "AppStaticVariableOptions");

            migrationBuilder.AlterColumn<int>(
                name: "DinamicVariableId",
                table: "AppStaticVariableOptions",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStaticVariableOptions_AppDinamicVariables_DinamicVariableId",
                table: "AppStaticVariableOptions",
                column: "DinamicVariableId",
                principalTable: "AppDinamicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
