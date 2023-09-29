using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcompromisetimeline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCompromiseTimeLines",
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
                    CompromiseId = table.Column<long>(type: "BIGINT", nullable: false),
                    PhaseId = table.Column<int>(type: "INT", nullable: false),
                    MilestoneId = table.Column<int>(type: "INT", nullable: false),
                    ProyectedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    CompletedTime = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Observation = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCompromiseTimeLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCompromiseTimeLines_AppCompromises_CompromiseId",
                        column: x => x.CompromiseId,
                        principalTable: "AppCompromises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCompromiseTimeLines_AppParameter_MilestoneId",
                        column: x => x.MilestoneId,
                        principalTable: "AppParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AppCompromiseTimeLines_AppParameter_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "AppParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseTimeLines_CompromiseId",
                table: "AppCompromiseTimeLines",
                column: "CompromiseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseTimeLines_MilestoneId",
                table: "AppCompromiseTimeLines",
                column: "MilestoneId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseTimeLines_PhaseId",
                table: "AppCompromiseTimeLines",
                column: "PhaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCompromiseTimeLines");
        }
    }
}
