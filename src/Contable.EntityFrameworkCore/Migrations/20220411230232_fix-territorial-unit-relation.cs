using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixterritorialunitrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppDepartments_DepartmentId",
                table: "AppTerritorialUnitDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppTerritorialUnits_TerritorialUnitId",
                table: "AppTerritorialUnitDepartments");

            migrationBuilder.AlterColumn<int>(
                name: "TerritorialUnitId",
                table: "AppTerritorialUnitDepartments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "AppTerritorialUnitDepartments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppDepartments_DepartmentId",
                table: "AppTerritorialUnitDepartments",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppTerritorialUnits_TerritorialUnitId",
                table: "AppTerritorialUnitDepartments",
                column: "TerritorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppDepartments_DepartmentId",
                table: "AppTerritorialUnitDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppTerritorialUnits_TerritorialUnitId",
                table: "AppTerritorialUnitDepartments");

            migrationBuilder.AlterColumn<int>(
                name: "TerritorialUnitId",
                table: "AppTerritorialUnitDepartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "AppTerritorialUnitDepartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppDepartments_DepartmentId",
                table: "AppTerritorialUnitDepartments",
                column: "DepartmentId",
                principalTable: "AppDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppTerritorialUnitDepartments_AppTerritorialUnits_TerritorialUnitId",
                table: "AppTerritorialUnitDepartments",
                column: "TerritorialUnitId",
                principalTable: "AppTerritorialUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
