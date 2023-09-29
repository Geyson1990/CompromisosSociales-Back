using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addterritorialunitcoordinators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AppPersons",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppTerritorialUnitCoordinators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    PersonId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTerritorialUnitCoordinators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTerritorialUnitCoordinators_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTerritorialUnitCoordinators_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTerritorialUnitCoordinators_PersonId",
                table: "AppTerritorialUnitCoordinators",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTerritorialUnitCoordinators_TerritorialUnitId",
                table: "AppTerritorialUnitCoordinators",
                column: "TerritorialUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTerritorialUnitCoordinators");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppPersons");
        }
    }
}
