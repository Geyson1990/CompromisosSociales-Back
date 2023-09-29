using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSocialConflictLocationDeleteFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppSocialConflictLocations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AppSocialConflictLocations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AppSocialConflictLocations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppSocialConflictLocations",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppSocialConflictLocations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppSocialConflictLocations",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "AppSocialConflictLocations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppSocialConflictLocations");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "AppSocialConflictLocations");
        }
    }
}
