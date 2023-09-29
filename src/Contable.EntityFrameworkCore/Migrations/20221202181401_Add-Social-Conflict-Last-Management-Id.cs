using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSocialConflictLastManagementId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastSocialConflictManagementId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSocialConflictManagementId",
                table: "AppSocialConflicts");
        }
    }
}
