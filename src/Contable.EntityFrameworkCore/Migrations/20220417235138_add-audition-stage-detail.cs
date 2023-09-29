using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addauditionstagedetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProjectStageDetails_ProjectStageId_StaticVariableId",
                table: "AppProjectStageDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppProjectStageDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AppProjectStageDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "AppProjectStageDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppProjectStageDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppProjectStageDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppProjectStageDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "AppProjectStageDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectStageDetails_ProjectStageId",
                table: "AppProjectStageDetails",
                column: "ProjectStageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppProjectStageDetails_ProjectStageId",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppProjectStageDetails");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "AppProjectStageDetails");

            migrationBuilder.CreateIndex(
                name: "IX_AppProjectStageDetails_ProjectStageId_StaticVariableId",
                table: "AppProjectStageDetails",
                columns: new[] { "ProjectStageId", "StaticVariableId" });
        }
    }
}
