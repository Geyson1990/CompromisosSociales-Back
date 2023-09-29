using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppRisks_RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.CreateTable(
                name: "AppSocialConflictRisks",
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
                    RiskId = table.Column<int>(type: "INT", nullable: false),
                    RiskTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictRisks_AppRisks_RiskId",
                        column: x => x.RiskId,
                        principalTable: "AppRisks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictRisks_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictRisks_RiskId",
                table: "AppSocialConflictRisks",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictRisks_SocialConflictId",
                table: "AppSocialConflictRisks",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictRisks");

            migrationBuilder.AddColumn<int>(
                name: "RiskId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_RiskId",
                table: "AppSocialConflicts",
                column: "RiskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppRisks_RiskId",
                table: "AppSocialConflicts",
                column: "RiskId",
                principalTable: "AppRisks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
