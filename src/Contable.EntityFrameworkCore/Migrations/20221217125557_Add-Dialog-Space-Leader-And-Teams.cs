using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceLeaderAndTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDialogSpaceLeaders",
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
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: false),
                    Index = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceLeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLeaders_AppDialogSpaces_DialogSpaceId",
                        column: x => x.DialogSpaceId,
                        principalTable: "AppDialogSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceLeaders_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppDialogSpaceTeams",
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
                    DialogSpaceLeaderId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDialogSpaceTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDialogSpaceTeams_AppDialogSpaceLeaders_DialogSpaceLeaderId",
                        column: x => x.DialogSpaceLeaderId,
                        principalTable: "AppDialogSpaceLeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLeaders_DialogSpaceId",
                table: "AppDialogSpaceLeaders",
                column: "DialogSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceLeaders_DirectoryGovernmentId",
                table: "AppDialogSpaceLeaders",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaceTeams_DialogSpaceLeaderId",
                table: "AppDialogSpaceTeams",
                column: "DialogSpaceLeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDialogSpaceTeams");

            migrationBuilder.DropTable(
                name: "AppDialogSpaceLeaders");
        }
    }
}
