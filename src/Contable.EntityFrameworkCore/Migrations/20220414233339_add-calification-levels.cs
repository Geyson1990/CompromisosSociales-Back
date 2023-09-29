using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcalificationlevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Min = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Max = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false),
                    Color = table.Column<string>(type: "VARCHAR(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLevels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLevels");
        }
    }
}
