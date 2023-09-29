using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceDocumentAndDocumentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDialogSpaceDocumentTypes",
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
                    table.PrimaryKey("PK_AppDialogSpaceDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDialogSpaceDocuments",
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
                    DialogSpaceId = table.Column<int>(type: "INT", nullable: false),
                    DialogSpaceDocumentTypeId = table.Column<int>(type: "INT", nullable: false),
                    Document = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    DocumentTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceDocuments_AppDialogSpaceDocumentTypes_DialogSpaceDocumentTypeId",
                        column: x => x.DialogSpaceDocumentTypeId,
                        principalTable: "AppDialogSpaceDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceDocuments_AppDialogSpaces_DialogSpaceId",
                        column: x => x.DialogSpaceId,
                        principalTable: "AppDialogSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceDocuments_DialogSpaceDocumentTypeId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceDocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceDocuments_DialogSpaceId",
                table: "AppDialogSpaceDocuments",
                column: "DialogSpaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDialogSpaceDocuments");

            migrationBuilder.DropTable(
                name: "AppDialogSpaceDocumentTypes");
        }
    }
}
