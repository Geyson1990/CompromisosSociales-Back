using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsugerenceaccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "AppSocialConflictSugerences",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "AcceptedUserId",
                table: "AppSocialConflictSugerences",
                type: "BIGINT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "AppSocialConflictSugerences");

            migrationBuilder.DropColumn(
                name: "AcceptedUserId",
                table: "AppSocialConflictSugerences");
        }
    }
}
