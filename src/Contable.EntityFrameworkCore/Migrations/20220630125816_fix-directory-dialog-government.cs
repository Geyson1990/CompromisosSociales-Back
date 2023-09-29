using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixdirectorydialoggovernment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_GovernmentId",
                table: "AppDirectoryDialogs");

            migrationBuilder.RenameColumn(
                name: "GovernmentId",
                table: "AppDirectoryDialogs",
                newName: "DirectoryGovernmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDirectoryDialogs_GovernmentId",
                table: "AppDirectoryDialogs",
                newName: "IX_AppDirectoryDialogs_DirectoryGovernmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_DirectoryGovernmentId",
                table: "AppDirectoryDialogs",
                column: "DirectoryGovernmentId",
                principalTable: "AppDirectoryGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_DirectoryGovernmentId",
                table: "AppDirectoryDialogs");

            migrationBuilder.RenameColumn(
                name: "DirectoryGovernmentId",
                table: "AppDirectoryDialogs",
                newName: "GovernmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AppDirectoryDialogs_DirectoryGovernmentId",
                table: "AppDirectoryDialogs",
                newName: "IX_AppDirectoryDialogs_GovernmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_GovernmentId",
                table: "AppDirectoryDialogs",
                column: "GovernmentId",
                principalTable: "AppDirectoryGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
