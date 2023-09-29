using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class altercoordinatormanageranalysttable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppPersons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AppPersons",
                type: "INT",
                nullable: true);
        }
    }
}
