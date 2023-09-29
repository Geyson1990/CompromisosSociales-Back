using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixsocialconflicttask1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "AppSocialConflictTaskManagementExtends",
                type: "DATETIME",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "AppSocialConflictTaskManagementComments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "AppSocialConflictTaskManagementExtends",
                type: "DATETIME",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "DATETIME");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "AppSocialConflictTaskManagementComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments",
                column: "Id");
        }
    }
}
