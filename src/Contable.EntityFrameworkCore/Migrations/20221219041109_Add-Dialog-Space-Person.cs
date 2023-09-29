using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpacePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AppDialogSpaces",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDialogSpaces_PersonId",
                table: "AppDialogSpaces",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDialogSpaces_AppPersons_PersonId",
                table: "AppDialogSpaces",
                column: "PersonId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDialogSpaces_AppPersons_PersonId",
                table: "AppDialogSpaces");

            migrationBuilder.DropIndex(
                name: "IX_AppDialogSpaces_PersonId",
                table: "AppDialogSpaces");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AppDialogSpaces");
        }
    }
}
