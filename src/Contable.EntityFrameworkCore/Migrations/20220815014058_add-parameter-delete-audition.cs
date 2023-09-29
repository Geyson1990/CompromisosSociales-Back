using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addparameterdeleteaudition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AppParameterCategory",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppParameterCategory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppParameterCategory",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AppParameter",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppParameter",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppParameter",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AppParameterCategory");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppParameterCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppParameterCategory");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AppParameter");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppParameter");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppParameter");
        }
    }
}
