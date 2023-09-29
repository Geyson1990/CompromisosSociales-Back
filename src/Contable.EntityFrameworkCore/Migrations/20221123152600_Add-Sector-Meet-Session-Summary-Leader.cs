using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionSummaryLeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionSummaries_SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries",
                column: "SectorMeetSessionLeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeetSessionSummaries_AppSectorMeetSessionLeaders_SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries",
                column: "SectorMeetSessionLeaderId",
                principalTable: "AppSectorMeetSessionLeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeetSessionSummaries_AppSectorMeetSessionLeaders_SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries");

            migrationBuilder.DropIndex(
                name: "IX_AppSectorMeetSessionSummaries_SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries");

            migrationBuilder.DropColumn(
                name: "SectorMeetSessionLeaderId",
                table: "AppSectorMeetSessionSummaries");
        }
    }
}
