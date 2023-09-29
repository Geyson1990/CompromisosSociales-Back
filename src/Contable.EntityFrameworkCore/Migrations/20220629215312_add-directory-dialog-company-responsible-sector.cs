using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddirectorydialogcompanyresponsiblesector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDirectoryResponsibles",
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
                    table.PrimaryKey("PK_AppDirectoryResponsibles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDirectorySectors",
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
                    table.PrimaryKey("PK_AppDirectorySectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDirectoryDialogs",
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
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    FirstSurname = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    SecondSurname = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Job = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    EmailAddress = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    MobilePhoneNumber = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    DirectoryResponsibleId = table.Column<int>(type: "INT", nullable: false),
                    GovernmentId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDirectoryDialogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDirectoryDialogs_AppDirectoryResponsibles_DirectoryResponsibleId",
                        column: x => x.DirectoryResponsibleId,
                        principalTable: "AppDirectoryResponsibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDirectoryDialogs_AppGovernments_GovernmentId",
                        column: x => x.GovernmentId,
                        principalTable: "AppGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppDirectoryIndustries",
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
                    Name = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(300)", nullable: true),
                    EmailAddress = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Url = table.Column<string>(type: "VARCHAR(150)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    DistrictId = table.Column<int>(type: "INT", nullable: false),
                    DirectorySectorId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDirectoryIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDirectoryIndustries_AppDirectorySectors_DirectorySectorId",
                        column: x => x.DirectorySectorId,
                        principalTable: "AppDirectorySectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppDirectoryIndustries_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryDialogs_DirectoryResponsibleId",
                table: "AppDirectoryDialogs",
                column: "DirectoryResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryDialogs_GovernmentId",
                table: "AppDirectoryDialogs",
                column: "GovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryIndustries_DirectorySectorId",
                table: "AppDirectoryIndustries",
                column: "DirectorySectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryIndustries_DistrictId",
                table: "AppDirectoryIndustries",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDirectoryDialogs");

            migrationBuilder.DropTable(
                name: "AppDirectoryIndustries");

            migrationBuilder.DropTable(
                name: "AppDirectoryResponsibles");

            migrationBuilder.DropTable(
                name: "AppDirectorySectors");
        }
    }
}
