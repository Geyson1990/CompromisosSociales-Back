using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflicttasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagements",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: true),
                    SocialConflictAlertId = table.Column<int>(type: "INT", nullable: true),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: true),
                    Title = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Deadline = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Status = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictTaskManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagements_AppSocialConflictAlerts_SocialConflictAlertId",
                        column: x => x.SocialConflictAlertId,
                        principalTable: "AppSocialConflictAlerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagements_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagements_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagementComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    SocialConflictTaskManagementId = table.Column<int>(type: "INT", nullable: false),
                    UserId = table.Column<long>(type: "BIGINT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Type = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictTaskManagementComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementComments_AppSocialConflictTaskManagements_SocialConflictTaskManagementId",
                        column: x => x.SocialConflictTaskManagementId,
                        principalTable: "AppSocialConflictTaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementComments_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagementExtends",
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
                    Description = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictTaskManagementExtends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementExtends_AppSocialConflictTaskManagements_SocialConflictTaskManagementId",
                        column: x => x.SocialConflictTaskManagementId,
                        principalTable: "AppSocialConflictTaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSocialConflictTaskManagementPersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialConflictTaskManagementId = table.Column<int>(type: "INT", nullable: false),
                    PersonId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictTaskManagementPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementPersons_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictTaskManagementPersons_AppSocialConflictTaskManagements_SocialConflictTaskManagementId",
                        column: x => x.SocialConflictTaskManagementId,
                        principalTable: "AppSocialConflictTaskManagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementComments_SocialConflictTaskManagementId",
                table: "AppSocialConflictTaskManagementComments",
                column: "SocialConflictTaskManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementComments_UserId",
                table: "AppSocialConflictTaskManagementComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementExtends_SocialConflictTaskManagementId",
                table: "AppSocialConflictTaskManagementExtends",
                column: "SocialConflictTaskManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementPersons_PersonId",
                table: "AppSocialConflictTaskManagementPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagementPersons_SocialConflictTaskManagementId",
                table: "AppSocialConflictTaskManagementPersons",
                column: "SocialConflictTaskManagementId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagements_SocialConflictAlertId",
                table: "AppSocialConflictTaskManagements",
                column: "SocialConflictAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagements_SocialConflictId",
                table: "AppSocialConflictTaskManagements",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictTaskManagements_SocialConflictSensibleId",
                table: "AppSocialConflictTaskManagements",
                column: "SocialConflictSensibleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagementComments");

            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagementExtends");

            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagementPersons");

            migrationBuilder.DropTable(
                name: "AppSocialConflictTaskManagements");
        }
    }
}
