using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlterRecordAndSocialConflictCodeReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_Code",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppRecords_Code",
                table: "AppRecords");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_Code",
                table: "AppCompromises");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_Code",
                table: "AppSocialConflicts",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AppRecords_Code",
                table: "AppRecords",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_Code",
                table: "AppCompromises",
                column: "Code");
        }
    }
}
