using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class removetmraddsctm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTaskManagementResources");

            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagementResources",
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
                    SocialConflictTaskManagementId = table.Column<int>(type: "INT", nullable: false),
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
                    table.PrimaryKey("PK_AppSocialConflictTaskManagementResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementResources_AppSocialConflictTaskManagements_SocialConflictTaskManagementId",
                        column: x => x.SocialConflictTaskManagementId,
                        principalTable: "AppSocialConflictTaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementResources_SocialConflictTaskManagementId",
                table: "AppSocialConflictTaskManagementResources",
                column: "SocialConflictTaskManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagementResources");

            migrationBuilder.CreateTable(
                name: "AppTaskManagementResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    CommonFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    FileName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Resource = table.Column<string>(type: "TEXT", nullable: true),
                    ResourceFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SectionFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Size = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    TaskManagementId = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTaskManagementResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTaskManagementResources_AppTaskManagement_TaskManagementId",
                        column: x => x.TaskManagementId,
                        principalTable: "AppTaskManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTaskManagementResources_TaskManagementId",
                table: "AppTaskManagementResources",
                column: "TaskManagementId");
        }
    }
}
