using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class altercoordinatormanageranalysttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAnalystTerritorialUnits");

            migrationBuilder.DropTable(
                name: "AppPersonTerritorialUnits");

            migrationBuilder.DropTable(
                name: "AppSubPersons");

            migrationBuilder.DropTable(
                name: "AppAnalysts");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AppPersons",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppPersons",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppPersons");

            migrationBuilder.CreateTable(
                name: "AppAnalysts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnalysts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPersonTerritorialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enabled = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    PersonId = table.Column<int>(type: "INT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AppAnalystTerritorialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalystId = table.Column<int>(type: "INT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnalystTerritorialUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAnalystTerritorialUnits_AppAnalysts_AnalystId",
                        column: x => x.AnalystId,
                        principalTable: "AppAnalysts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAnalystTerritorialUnits_AnalystId",
                table: "AppAnalystTerritorialUnits",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_AppAnalystTerritorialUnits_TerritorialUnitId",
                table: "AppAnalystTerritorialUnits",
                column: "TerritorialUnitId");

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
    }
}
