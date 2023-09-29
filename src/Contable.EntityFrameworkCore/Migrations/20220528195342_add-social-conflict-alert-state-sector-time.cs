using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertstatesectortime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertStates",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StateTime",
                table: "AppSocialConflictAlertStates",
                type: "DATETIME",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertSectors",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SectorTime",
                table: "AppSocialConflictAlertSectors",
                type: "DATETIME",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendations",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Information",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(6000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Demand",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AditionalInformation",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actions",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateTime",
                table: "AppSocialConflictAlertStates");

            migrationBuilder.DropColumn(
                name: "SectorTime",
                table: "AppSocialConflictAlertSectors");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertStates",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertSectors",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Recommendations",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Information",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(6000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Demand",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AditionalInformation",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actions",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);
        }
    }
}
