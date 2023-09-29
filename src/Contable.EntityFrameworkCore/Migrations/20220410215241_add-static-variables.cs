using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addstaticvariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppStaticVariables",
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
                    Name = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStaticVariables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppStaticVariableOptions",
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
                    StaticVariableId = table.Column<int>(type: "INT", nullable: false),
                    DinamicVariableId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    Type = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStaticVariableOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStaticVariableOptions_AppDinamicVariables_DinamicVariableId",
                        column: x => x.DinamicVariableId,
                        principalTable: "AppDinamicVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStaticVariableOptions_AppStaticVariables_StaticVariableId",
                        column: x => x.StaticVariableId,
                        principalTable: "AppStaticVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppStaticVariableOptionDetails",
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
                    StaticVariableId = table.Column<int>(type: "INT", nullable: false),
                    StaticVariableOptionId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Value = table.Column<decimal>(type: "DECIMAL(27,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStaticVariableOptionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStaticVariableOptionDetails_AppStaticVariables_StaticVariableId",
                        column: x => x.StaticVariableId,
                        principalTable: "AppStaticVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppStaticVariableOptionDetails_AppStaticVariableOptions_StaticVariableOptionId",
                        column: x => x.StaticVariableOptionId,
                        principalTable: "AppStaticVariableOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppStaticVariableOptionDetails_StaticVariableId",
                table: "AppStaticVariableOptionDetails",
                column: "StaticVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaticVariableOptionDetails_StaticVariableOptionId",
                table: "AppStaticVariableOptionDetails",
                column: "StaticVariableOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaticVariableOptions_DinamicVariableId",
                table: "AppStaticVariableOptions",
                column: "DinamicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStaticVariableOptions_StaticVariableId",
                table: "AppStaticVariableOptions",
                column: "StaticVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppStaticVariableOptionDetails");

            migrationBuilder.DropTable(
                name: "AppStaticVariableOptions");

            migrationBuilder.DropTable(
                name: "AppStaticVariables");
        }
    }
}
