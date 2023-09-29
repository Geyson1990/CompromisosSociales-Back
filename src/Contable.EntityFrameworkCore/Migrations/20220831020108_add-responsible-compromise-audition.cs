using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addresponsiblecompromiseaudition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppCompromiseInvolved",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AppCompromiseInvolved",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AppCompromiseInvolved",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppCompromiseInvolved",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppCompromiseInvolved",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppCompromiseInvolved",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "AppCompromiseInvolved",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "AppCompromiseInvolved");
        }
    }
}
