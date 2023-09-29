using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class editgovernmentfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDirectoryDialogs_AppGovernments_GovernmentId",
                table: "AppDirectoryDialogs");

            migrationBuilder.DropTable(
                name: "AppGovernments");

            migrationBuilder.CreateTable(
                name: "AppDirectoryGovernmentSectors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_AppDirectoryGovernmentSectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDirectoryGovernments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
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
                    DirectoryGovernmentSectorId = table.Column<int>(type: "INT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDirectoryGovernments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDirectoryGovernments_AppDirectoryGovernmentSectors_DirectoryGovernmentSectorId",
                        column: x => x.DirectoryGovernmentSectorId,
                        principalTable: "AppDirectoryGovernmentSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDirectoryGovernments_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryGovernments_DirectoryGovernmentSectorId",
                table: "AppDirectoryGovernments",
                column: "DirectoryGovernmentSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryGovernments_DistrictId",
                table: "AppDirectoryGovernments",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_GovernmentId",
                table: "AppDirectoryDialogs",
                column: "GovernmentId",
                principalTable: "AppDirectoryGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDirectoryDialogs_AppDirectoryGovernments_GovernmentId",
                table: "AppDirectoryDialogs");

            migrationBuilder.DropTable(
                name: "AppDirectoryGovernments");

            migrationBuilder.DropTable(
                name: "AppDirectoryGovernmentSectors");

            migrationBuilder.CreateTable(
                name: "AppGovernments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdditionalInformation = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Alias = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    SectorId = table.Column<int>(type: "INT", nullable: false),
                    ShortName = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(300)", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_AppDirectoryDialogs_AppGovernments_GovernmentId",
                table: "AppDirectoryDialogs",
                column: "GovernmentId",
                principalTable: "AppGovernments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
