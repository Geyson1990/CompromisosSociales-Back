using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionResourcesTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "AppSectorMeetSessionResources",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterTime",
                table: "AppSectorMeetSessionResources",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.DropColumn(
                name: "RegisterTime",
                table: "AppSectorMeetSessionResources");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "AppSectorMeetSessionResources",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                table: "AppSectorMeetSessionResources",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
