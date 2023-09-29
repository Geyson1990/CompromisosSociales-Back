using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class removesocialconflictsubsectorobservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppSubSectors_SubSectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropTable(
                name: "AppSocialConflictObservations");

            migrationBuilder.DropTable(
                name: "AppSubSectors");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_SubSectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "SubSectorId",
                table: "AppSocialConflicts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubSectorId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppSocialConflictObservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(1000)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    SocialConflictId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictObservations_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSubSectors",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SectorId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubSectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubSectors_AppSectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "AppSectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_SubSectorId",
                table: "AppSocialConflicts",
                column: "SubSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictObservations_SocialConflictId",
                table: "AppSocialConflictObservations",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubSectors_SectorId",
                table: "AppSubSectors",
                column: "SectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppSubSectors_SubSectorId",
                table: "AppSocialConflicts",
                column: "SubSectorId",
                principalTable: "AppSubSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
