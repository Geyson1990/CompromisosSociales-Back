using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionSummaryAndResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorMeetId",
                table: "AppSectorMeetSessions",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionResources",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    PersonId = table.Column<int>(type: "INT", nullable: false),
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
                    table.PrimaryKey("PK_AppSectorMeetSessionResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionResources_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionResources_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionSummaries",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionSummaries_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessions_SectorMeetId",
                table: "AppSectorMeetSessions",
                column: "SectorMeetId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionResources_PersonId",
                table: "AppSectorMeetSessionResources",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionResources_SectorMeetSessionId",
                table: "AppSectorMeetSessionResources",
                column: "SectorMeetSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionSummaries_SectorMeetSessionId",
                table: "AppSectorMeetSessionSummaries",
                column: "SectorMeetSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessions_AppSectorMeets_SectorMeetId",
                table: "AppSectorMeetSessions",
                column: "SectorMeetId",
                principalTable: "AppSectorMeets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessions_AppSectorMeets_SectorMeetId",
                table: "AppSectorMeetSessions");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionResources");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionSummaries");

            migrationBuilder.DropIndex(
                name: "IX_AppSectorMeetSessions_SectorMeetId",
                table: "AppSectorMeetSessions");

            migrationBuilder.DropColumn(
                name: "SectorMeetId",
                table: "AppSectorMeetSessions");
        }
    }
}
