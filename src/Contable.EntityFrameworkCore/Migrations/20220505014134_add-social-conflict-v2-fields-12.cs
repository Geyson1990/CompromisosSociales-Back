using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialConflictStates",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: false),
                    ManagerId = table.Column<int>(type: "INT", nullable: false),
                    StateTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(3000)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialConflictStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialConflictStates_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SocialConflictStates_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialConflictStates_ManagerId",
                table: "SocialConflictStates",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialConflictStates_SocialConflictId",
                table: "SocialConflictStates",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialConflictStates");
        }
    }
}
