using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class editsocialconflictverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflicts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }
    }
}
