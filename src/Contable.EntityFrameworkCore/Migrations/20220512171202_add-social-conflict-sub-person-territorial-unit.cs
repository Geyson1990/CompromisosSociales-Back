using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsubpersonterritorialunit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppPersons");

            migrationBuilder.CreateTable(
                name: "AppPersonTerritorialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<int>(type: "INT", nullable: false),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPersonTerritorialUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPersonTerritorialUnits_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppPersonTerritorialUnits_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSubPersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PersonId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubPersons_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppPersonTerritorialUnits_PersonId",
                table: "AppPersonTerritorialUnits",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPersonTerritorialUnits_TerritorialUnitId",
                table: "AppPersonTerritorialUnits",
                column: "TerritorialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubPersons_PersonId",
                table: "AppSubPersons",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPersonTerritorialUnits");

            migrationBuilder.DropTable(
                name: "AppSubPersons");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppPersons",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }
    }
}
