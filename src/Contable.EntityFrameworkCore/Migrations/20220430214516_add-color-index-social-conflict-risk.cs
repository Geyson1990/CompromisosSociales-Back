using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcolorindexsocialconflictrisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "AppRisks",
                type: "VARCHAR(30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "AppRisks",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "AppRisks");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "AppRisks");
        }
    }
}
