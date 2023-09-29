using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertsector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAlertSectors",
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
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlertSectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlertSectors",
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
                    AlertSectorId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlertSectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertSectors_AppAlertSectors_AlertSectorId",
                        column: x => x.AlertSectorId,
                        principalTable: "AppAlertSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertSectors_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertSectors_AlertSectorId",
                table: "AppSocialConflictAlertSectors",
                column: "AlertSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertSectors_SocialConflictAlertId",
                table: "AppSocialConflictAlertSectors",
                column: "SocialConflictAlertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictAlertSectors");

            migrationBuilder.DropTable(
                name: "AppAlertSectors");
        }
    }
}
