using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertrisktime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "AppSocialConflictAlertRisks",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertRisks",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RiskTime",
                table: "AppSocialConflictAlertRisks",
                type: "DATETIME",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiskTime",
                table: "AppSocialConflictAlertRisks");

            migrationBuilder.AlterColumn<string>(
                name: "Observation",
                table: "AppSocialConflictAlertRisks",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertRisks",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);
        }
    }
}
