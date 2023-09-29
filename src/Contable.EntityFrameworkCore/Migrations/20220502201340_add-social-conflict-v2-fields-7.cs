using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictGeneralFacts",
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
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    FactTime = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictGeneralFacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictGeneralFacts_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictGeneralFacts_SocialConflictId",
                table: "AppSocialConflictGeneralFacts",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictGeneralFacts");
        }
    }
}
