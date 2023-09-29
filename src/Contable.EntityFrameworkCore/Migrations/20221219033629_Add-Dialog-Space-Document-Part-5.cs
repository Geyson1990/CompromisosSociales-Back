using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceDocumentPart5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxInstallationTime",
                table: "AppDialogSpaceDocuments",
                newName: "VigencyTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "InstallationMaxTime",
                table: "AppDialogSpaceDocuments",
                type: "DATETIME",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallationMaxTime",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.RenameColumn(
                name: "VigencyTime",
                table: "AppDialogSpaceDocuments",
                newName: "MaxInstallationTime");
        }
    }
}
