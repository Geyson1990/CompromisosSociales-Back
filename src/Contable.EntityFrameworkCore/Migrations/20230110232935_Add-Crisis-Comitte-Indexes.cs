using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddCrisisComitteIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeTeams",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeTasks",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeSectors",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteePlans",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeMessage",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeChannels",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeAgreements",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppCrisisCommitteeActions",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeTeams");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeTasks");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeSectors");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteePlans");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeMessage");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeChannels");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeAgreements");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppCrisisCommitteeActions");
        }
    }
}
