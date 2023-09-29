using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSocialConflictSensibleLastManagementAndState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastSocialConflictSensibleManagementId",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastSocialConflictSensibleStateId",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSocialConflictSensibleManagementId",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "LastSocialConflictSensibleStateId",
                table: "AppSocialConflictSensibles");
        }
    }
}
