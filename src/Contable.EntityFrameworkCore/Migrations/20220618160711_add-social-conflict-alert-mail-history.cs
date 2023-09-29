using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertmailhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter",
                table: "AppSocialConflictActors");

            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlertHistories",
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
                    SocialConflictAlertId = table.Column<int>(type: "INT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    Template = table.Column<string>(type: "TEXT", nullable: true),
                    To = table.Column<string>(type: "TEXT", nullable: true),
                    Copy = table.Column<string>(type: "TEXT", nullable: true),
                    Files = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlertHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertHistories_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertHistories_SocialConflictAlertId",
                table: "AppSocialConflictAlertHistories",
                column: "SocialConflictAlertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictAlertHistories");

            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "AppSocialConflictActors",
                type: "TEXT",
                nullable: true);
        }
    }
}
