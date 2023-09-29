using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlterDialogSpaceDocumentSituationFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Installation",
                table: "AppDialogSpaceDocumentSituations");

            migrationBuilder.DropColumn(
                name: "Vigency",
                table: "AppDialogSpaceDocumentSituations");

            migrationBuilder.AddColumn<DateTime>(
                name: "MaxInstallationTime",
                table: "AppDialogSpaceDocuments",
                type: "DATETIME",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxInstallationTime",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.AddColumn<bool>(
                name: "Installation",
                table: "AppDialogSpaceDocumentSituations",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vigency",
                table: "AppDialogSpaceDocumentSituations",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }
    }
}
