using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionLeaders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionLeaders",
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
                    Type = table.Column<int>(type: "INT", nullable: false),
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: true),
                    DirectoryIndustryId = table.Column<int>(type: "INT", nullable: true),
                    Entity = table.Column<string>(type: "VARCHAR(5000)", nullable: true),
                    Role = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionLeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionLeaders_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionLeaders_AppDirectoryIndustries_DirectoryIndustryId",
                        column: x => x.DirectoryIndustryId,
                        principalTable: "AppDirectoryIndustries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionLeaders_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionLeaders_DirectoryGovernmentId",
                table: "AppSectorMeetSessionLeaders",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionLeaders_DirectoryIndustryId",
                table: "AppSectorMeetSessionLeaders",
                column: "DirectoryIndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionLeaders_SectorMeetSessionId",
                table: "AppSectorMeetSessionLeaders",
                column: "SectorMeetSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionLeaders");
        }
    }
}
