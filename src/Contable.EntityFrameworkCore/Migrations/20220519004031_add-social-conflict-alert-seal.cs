using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertseal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAlertSeals",
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
                    table.PrimaryKey("PK_AppAlertSeals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlertSeals",
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
                    AlertSealId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlertSeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertSeals_AppAlertSeals_AlertSealId",
                        column: x => x.AlertSealId,
                        principalTable: "AppAlertSeals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertSeals_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertSeals_AlertSealId",
                table: "AppSocialConflictAlertSeals",
                column: "AlertSealId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertSeals_SocialConflictAlertId",
                table: "AppSocialConflictAlertSeals",
                column: "SocialConflictAlertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictAlertSeals");

            migrationBuilder.DropTable(
                name: "AppAlertSeals");
        }
    }
}
