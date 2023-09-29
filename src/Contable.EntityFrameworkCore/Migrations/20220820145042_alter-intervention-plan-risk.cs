using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterinterventionplanrisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiskTime",
                table: "AppInterventionPlanRisks");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppInterventionPlanRisks",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppInterventionPlanRisks",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RiskTime",
                table: "AppInterventionPlanRisks",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
