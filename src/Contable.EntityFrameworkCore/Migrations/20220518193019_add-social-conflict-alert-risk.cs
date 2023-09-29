using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertrisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAlertRisks",
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
                    Color = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlertRisks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictAlertRisks",
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
                    AlertRiskId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Observation = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictAlertRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertRisks_AppAlertRisks_AlertRiskId",
                        column: x => x.AlertRiskId,
                        principalTable: "AppAlertRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictAlertRisks_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertRisks_AlertRiskId",
                table: "AppSocialConflictAlertRisks",
                column: "AlertRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlertRisks_SocialConflictAlertId",
                table: "AppSocialConflictAlertRisks",
                column: "SocialConflictAlertId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictAlertRisks");

            migrationBuilder.DropTable(
                name: "AppAlertRisks");
        }
    }
}
