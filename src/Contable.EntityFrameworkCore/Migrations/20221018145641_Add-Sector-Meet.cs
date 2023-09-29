using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSectorMeets",
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
                    Code = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Year = table.Column<int>(type: "INT", nullable: false),
                    Count = table.Column<int>(type: "INT", nullable: false),
                    Generation = table.Column<bool>(type: "BIT", nullable: false),
                    MeetName = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    TerritorialUnitId = table.Column<int>(type: "INT", nullable: false),
                    SocialConflictId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeets_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeets_AppTerritorialUnits_TerritorialUnitId",
                        column: x => x.TerritorialUnitId,
                        principalTable: "AppTerritorialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeets_SocialConflictId",
                table: "AppSectorMeets",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeets_TerritorialUnitId",
                table: "AppSectorMeets",
                column: "TerritorialUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSectorMeets");
        }
    }
}
