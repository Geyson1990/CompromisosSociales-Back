using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddistrictregioncode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "AppRegions",
                type: "VARCHAR(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "AppRegions");
        }
    }
}
