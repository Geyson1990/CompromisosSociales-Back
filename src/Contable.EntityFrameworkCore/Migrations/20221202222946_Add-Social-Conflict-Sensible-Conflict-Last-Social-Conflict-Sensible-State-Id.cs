using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSocialConflictSensibleConflictLastSocialConflictSensibleStateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastSocialConflictSensibleConditionId",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSocialConflictSensibleConditionId",
                table: "AppSocialConflictSensibles");
        }
    }
}
