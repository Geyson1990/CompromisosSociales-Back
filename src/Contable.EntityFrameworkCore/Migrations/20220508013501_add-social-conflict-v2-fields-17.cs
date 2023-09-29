using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "AppSocialConflictActors",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter",
                table: "AppSocialConflictActors");
        }
    }
}
