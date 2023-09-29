using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class altersocialconflictlastcondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastConditionId",
                table: "AppSocialConflicts");

            migrationBuilder.AddColumn<int>(
                name: "LastCondition",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCondition",
                table: "AppSocialConflicts");

            migrationBuilder.AddColumn<int>(
                name: "LastConditionId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);
        }
    }
}
