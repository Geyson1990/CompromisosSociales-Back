using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddUserResetPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordResetCode",
                table: "AbpUsers",
                type: "VARCHAR(32)",
                maxLength: 328,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(328)",
                oldMaxLength: 328,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailConfirmationCode",
                table: "AbpUsers",
                type: "VARCHAR(32)",
                maxLength: 328,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(328)",
                oldMaxLength: 328,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmationTime",
                table: "AbpUsers",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetTime",
                table: "AbpUsers",
                type: "DATETIME",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmationTime",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PasswordResetTime",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordResetCode",
                table: "AbpUsers",
                type: "nvarchar(328)",
                maxLength: 328,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(32)",
                oldMaxLength: 328,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailConfirmationCode",
                table: "AbpUsers",
                type: "nvarchar(328)",
                maxLength: 328,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(32)",
                oldMaxLength: 328,
                oldNullable: true);
        }
    }
}
