using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddinamicvariable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDinamicVariables",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDinamicVariables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDinamicVariableDetails",
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
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    DinamicVariableId = table.Column<int>(type: "INT", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDinamicVariableDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDinamicVariableDetails_AppDinamicVariables_DinamicVariableId",
                        column: x => x.DinamicVariableId,
                        principalTable: "AppDinamicVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppDinamicVariableDetails_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDinamicVariableDetails_DinamicVariableId",
                table: "AppDinamicVariableDetails",
                column: "DinamicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDinamicVariableDetails_DistrictId",
                table: "AppDinamicVariableDetails",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDinamicVariableDetails");

            migrationBuilder.DropTable(
                name: "AppDinamicVariables");
        }
    }
}
