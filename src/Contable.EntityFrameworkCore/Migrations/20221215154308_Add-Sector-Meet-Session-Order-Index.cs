using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSessionOrderIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionSummaries",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionSchedules",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionLeaders",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionCriticalAspects",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionAgreements",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppSectorMeetSessionActions",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionSummaries");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionSchedules");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionLeaders");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionCriticalAspects");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionAgreements");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppSectorMeetSessionActions");
        }
    }
}
