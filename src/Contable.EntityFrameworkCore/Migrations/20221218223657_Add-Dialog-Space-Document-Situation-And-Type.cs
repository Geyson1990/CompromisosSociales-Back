using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceDocumentSituationAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AppDialogSpaces",
                newName: "DialogSpaceTypeId");

            migrationBuilder.AddColumn<int>(
                name: "DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Exposition",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationTime",
                table: "AppDialogSpaceDocuments",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "AppDialogSpaceDocuments",
                type: "VARCHAR(5000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VigencyDays",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VigencyRangeSide",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppDialogSpaceDocumentSituations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceDocumentSituations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDialogSpaceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaces_DialogSpaceTypeId",
                table: "AppDialogSpaces",
                column: "DialogSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceDocuments_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentSituationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentSituationId",
                principalTable: "AppDialogSpaceDocumentSituations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaces_AppDialogSpaceTypes_DialogSpaceTypeId",
                table: "AppDialogSpaces",
                column: "DialogSpaceTypeId",
                principalTable: "AppDialogSpaceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaces_AppDialogSpaceTypes_DialogSpaceTypeId",
                table: "AppDialogSpaces");

            migrationBuilder.DropTable(
                name: "AppDialogSpaceDocumentSituations");

            migrationBuilder.DropTable(
                name: "AppDialogSpaceTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppDialogSpaces_DialogSpaceTypeId",
                table: "AppDialogSpaces");

            migrationBuilder.DropIndex(
                name: "IX_AppDialogSpaceDocuments_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "Exposition",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "InstallationTime",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "VigencyDays",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "VigencyRangeSide",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.RenameColumn(
                name: "DialogSpaceTypeId",
                table: "AppDialogSpaces",
                newName: "Type");
        }
    }
}
