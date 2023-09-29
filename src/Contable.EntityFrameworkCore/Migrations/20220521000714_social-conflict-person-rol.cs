using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class socialconflictpersonrol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AbpUsers",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AbpUsers",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_PersonId",
                table: "AbpUsers",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppPersons_PersonId",
                table: "AbpUsers",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppPersons_PersonId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_PersonId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AbpUsers");
        }
    }
}
