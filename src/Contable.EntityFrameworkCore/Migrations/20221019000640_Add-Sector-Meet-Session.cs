using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessions",
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
                    SessionTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Type = table.Column<int>(type: "INT", nullable: false),
                    DepartmentId = table.Column<int>(type: "INT", nullable: true),
                    ProvinceId = table.Column<int>(type: "INT", nullable: true),
                    DistrictId = table.Column<int>(type: "INT", nullable: true),
                    Location = table.Column<string>(type: "VARCHAR(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessions_AppDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessions_AppDistricts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "AppDistricts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessions_AppProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "AppProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionActions",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionActions_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionAgreements",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionAgreements_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionCriticalAspects",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionCriticalAspects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionCriticalAspects_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSectorMeetSessionSchedules",
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
                    SectorMeetSessionId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSectorMeetSessionSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSectorMeetSessionSchedules_AppSectorMeetSessions_SectorMeetSessionId",
                        column: x => x.SectorMeetSessionId,
                        principalTable: "AppSectorMeetSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionActions_SectorMeetSessionId",
                table: "AppSectorMeetSessionActions",
                column: "SectorMeetSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionAgreements_SectorMeetSessionId",
                table: "AppSectorMeetSessionAgreements",
                column: "SectorMeetSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionCriticalAspects_SectorMeetSessionId",
                table: "AppSectorMeetSessionCriticalAspects",
                column: "SectorMeetSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessions_DepartmentId",
                table: "AppSectorMeetSessions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessions_DistrictId",
                table: "AppSectorMeetSessions",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessions_ProvinceId",
                table: "AppSectorMeetSessions",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSectorMeetSessionSchedules_SectorMeetSessionId",
                table: "AppSectorMeetSessionSchedules",
                column: "SectorMeetSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionActions");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionAgreements");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionCriticalAspects");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessionSchedules");

            migrationBuilder.DropTable(
                name: "AppSectorMeetSessions");
        }
    }
}
