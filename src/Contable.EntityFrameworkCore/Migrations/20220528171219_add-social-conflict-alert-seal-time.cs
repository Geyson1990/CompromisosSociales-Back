using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertsealtime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertSeals",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SealTime",
                table: "AppSocialConflictAlertSeals",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SealTime",
                table: "AppSocialConflictAlertSeals");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictAlertSeals",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);
        }
    }
}
