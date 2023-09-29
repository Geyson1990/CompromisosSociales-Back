using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addtaskmanagementhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTaskManagementHistories",
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
                    TaskManagementId = table.Column<long>(type: "BIGINT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Template = table.Column<string>(type: "TEXT", nullable: true),
                    To = table.Column<string>(type: "TEXT", nullable: true),
                    Copy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTaskManagementHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTaskManagementHistories_AppTaskManagement_TaskManagementId",
                        column: x => x.TaskManagementId,
                        principalTable: "AppTaskManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTaskManagementHistories_TaskManagementId",
                table: "AppTaskManagementHistories",
                column: "TaskManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTaskManagementHistories");
        }
    }
}
