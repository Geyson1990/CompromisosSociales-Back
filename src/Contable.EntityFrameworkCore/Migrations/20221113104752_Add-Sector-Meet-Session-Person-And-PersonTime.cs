using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionPersonAndPersonTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.DropIndex(
                name: "IX_AppSectorMeetSessionResources_PersonId",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.DropColumn(
                name: "RegisterTime",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppSectorMeetSessions",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonTime",
                table: "AppSectorMeetSessions",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessions_PersonId",
                table: "AppSectorMeetSessions",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessions_AppPersons_PersonId",
                table: "AppSectorMeetSessions",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessions_AppPersons_PersonId",
                table: "AppSectorMeetSessions");

            migrationBuilder.DropIndex(
                name: "IX_AppSectorMeetSessions_PersonId",
                table: "AppSectorMeetSessions");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppSectorMeetSessions");

            migrationBuilder.DropColumn(
                name: "PersonTime",
                table: "AppSectorMeetSessions");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppSectorMeetSessionResources",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterTime",
                table: "AppSectorMeetSessionResources",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionResources_PersonId",
                table: "AppSectorMeetSessionResources",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
