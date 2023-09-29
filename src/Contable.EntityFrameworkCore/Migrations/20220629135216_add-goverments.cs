using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addgoverments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppGovernments",
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
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    ShortName = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Alias = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    SectorId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGovernments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppGovernments_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppGovernments_AppSectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "AppSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppGovernments_DistrictId",
                table: "AppGovernments",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppGovernments_SectorId",
                table: "AppGovernments",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppGovernments");
        }
    }
}
