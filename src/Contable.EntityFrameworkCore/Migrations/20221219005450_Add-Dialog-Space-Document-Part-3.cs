using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceDocumentPart3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentTypes_DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentSituationId",
                principalTable: "AppDialogSpaceDocumentSituations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentTypes_DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentTypeId",
                principalTable: "AppDialogSpaceDocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentTypes_DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.AlterColumn<int>(
                name: "DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentSituations_DialogSpaceDocumentSituationId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentSituationId",
                principalTable: "AppDialogSpaceDocumentSituations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentTypes_DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentTypeId",
                principalTable: "AppDialogSpaceDocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
