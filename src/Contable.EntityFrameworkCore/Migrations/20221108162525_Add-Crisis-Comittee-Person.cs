using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddCrisisComitteePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppCrisisCommittees",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommittees_PersonId",
                table: "AppCrisisCommittees",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCrisisCommittees_AppPersons_PersonId",
                table: "AppCrisisCommittees",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCrisisCommittees_AppPersons_PersonId",
                table: "AppCrisisCommittees");

            migrationBuilder.DropIndex(
                name: "IX_AppCrisisCommittees_PersonId",
                table: "AppCrisisCommittees");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppCrisisCommittees");
        }
    }
}
