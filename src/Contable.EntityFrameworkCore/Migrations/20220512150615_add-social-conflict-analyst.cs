using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictanalyst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppAnalysts",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAnalysts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAnalystTerritorialUnits",
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
                    AnalystId = table.Column<int>(type: "INT", nullable: false),
                    TerrotorialUnitId = table.Column<int>(type: "INT", nullable: false)
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
                        name: "FK_AppAnalystTerritorialUnits_AppTerritorialUnits_TerrotorialUnitId",
                        column: x => x.TerrotorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAnalystTerritorialUnits_AnalystId",
                table: "AppAnalystTerritorialUnits",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_AppAnalystTerritorialUnits_TerrotorialUnitId",
                table: "AppAnalystTerritorialUnits",
                column: "TerrotorialUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAnalystTerritorialUnits");

            migrationBuilder.DropTable(
                name: "AppAnalysts");
        }
    }
}
