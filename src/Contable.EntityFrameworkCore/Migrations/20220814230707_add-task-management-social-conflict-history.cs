using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addtaskmanagementsocialconflicthistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagementHistories",
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
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Template = table.Column<string>(type: "TEXT", nullable: true),
                    To = table.Column<string>(type: "TEXT", nullable: true),
                    Copy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictTaskManagementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementHistories_AppSocialConflictTaskManagements_SocialConflictTaskManagementId",
                        column: x => x.SocialConflictTaskManagementId,
                        principalTable: "AppSocialConflictTaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementHistories_SocialConflictTaskManagementId",
                table: "AppSocialConflictTaskManagementHistories",
                column: "SocialConflictTaskManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagementHistories");
        }
    }
}
