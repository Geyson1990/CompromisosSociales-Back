using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionLeaderTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionTeams",
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
                    SectorMeetSessionLeaderId = table.Column<int>(type: "INT", nullable: false),
                    Document = table.Column<string>(type: "VARCHAR(32)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Surname = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SecondSurname = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Job = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    EmailAddress = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionTeams_AppSectorMeetSessionLeaders_SectorMeetSessionLeaderId",
                        column: x => x.SectorMeetSessionLeaderId,
                        principalTable: "AppSectorMeetSessionLeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionTeams_SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionTeams",
                column: "SectorMeetSessionLeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionTeams");
        }
    }
}
