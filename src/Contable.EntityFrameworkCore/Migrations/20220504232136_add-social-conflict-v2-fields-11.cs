using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictManagements",
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
                    ManagementId = table.Column<int>(type: "INT", nullable: false),
                    ManagerId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    ManagementTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CivilMen = table.Column<int>(type: "INT", nullable: true),
                    CivilWomen = table.Column<int>(type: "INT", nullable: true),
                    StateMen = table.Column<int>(type: "INT", nullable: true),
                    StateWomen = table.Column<int>(type: "INT", nullable: true),
                    CompanyMen = table.Column<int>(type: "INT", nullable: true),
                    CompanyWomen = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictManagements_AppManagements_ManagementId",
                        column: x => x.ManagementId,
                        principalTable: "AppManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictManagements_AppPersons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictManagements_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictManagementResources",
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
                    SocialConflictManagementId = table.Column<int>(type: "INT", nullable: false),
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
                    table.PrimaryKey("PK_AppSocialConflictManagementResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictManagementResources_AppSocialConflictManagements_SocialConflictManagementId",
                        column: x => x.SocialConflictManagementId,
                        principalTable: "AppSocialConflictManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictManagementResources_SocialConflictManagementId",
                table: "AppSocialConflictManagementResources",
                column: "SocialConflictManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictManagements_ManagementId",
                table: "AppSocialConflictManagements",
                column: "ManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictManagements_ManagerId",
                table: "AppSocialConflictManagements",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictManagements_SocialConflictId",
                table: "AppSocialConflictManagements",
                column: "SocialConflictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictManagementResources");

            migrationBuilder.DropTable(
                name: "AppSocialConflictManagements");
        }
    }
}
