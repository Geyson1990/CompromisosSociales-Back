using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalistId",
                table: "AppSocialConflicts");

            migrationBuilder.RenameColumn(
                name: "AnalistId",
                table: "AppSocialConflicts",
                newName: "AnalystId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflicts_AnalistId",
                table: "AppSocialConflicts",
                newName: "IX_AppSocialConflicts_AnalystId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalystId",
                table: "AppSocialConflicts",
                column: "AnalystId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalystId",
                table: "AppSocialConflicts");

            migrationBuilder.RenameColumn(
                name: "AnalystId",
                table: "AppSocialConflicts",
                newName: "AnalistId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflicts_AnalystId",
                table: "AppSocialConflicts",
                newName: "IX_AppSocialConflicts_AnalistId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalistId",
                table: "AppSocialConflicts",
                column: "AnalistId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
