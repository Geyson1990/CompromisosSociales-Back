using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addhelpmemories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppHelpMemories",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: true),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: true),
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    Generation = table.Column<bool>(type: "BIT", nullable: false),
                    Site = table.Column<int>(type: "INT", nullable: false),
                    Request = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHelpMemories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppHelpMemories_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppHelpMemories_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppHelpMemories_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppHelpMemoryResources",
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
                    HelpMemoryId = table.Column<int>(type: "INT", nullable: false),
                    CommonFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ResourceFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SectionFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    FileName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Size = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Extension = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ClassName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Resource = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHelpMemoryResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppHelpMemoryResources_AppHelpMemories_HelpMemoryId",
                        column: x => x.HelpMemoryId,
                        principalTable: "AppHelpMemories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppHelpMemories_DirectoryGovernmentId",
                table: "AppHelpMemories",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHelpMemories_SocialConflictId",
                table: "AppHelpMemories",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHelpMemories_SocialConflictSensibleId",
                table: "AppHelpMemories",
                column: "SocialConflictSensibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppHelpMemoryResources_HelpMemoryId",
                table: "AppHelpMemoryResources",
                column: "HelpMemoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppHelpMemoryResources");

            migrationBuilder.DropTable(
                name: "AppHelpMemories");
        }
    }
}
