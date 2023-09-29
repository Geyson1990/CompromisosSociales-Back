using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addcrisiscommitteecomplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeActions",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeActions_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeAgreements",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeAgreements_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeChannels",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeChannels_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeMessage",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeMessage_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteePlans",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteePlans_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeSectors",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    DirectoryGovernmentId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeSectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeSectors_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeSectors_AppDirectoryGovernments_DirectoryGovernmentId",
                        column: x => x.DirectoryGovernmentId,
                        principalTable: "AppDirectoryGovernments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppCrisisCommitteeTasks",
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
                    CrisisCommitteeId = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(5000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrisisCommitteeTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCrisisCommitteeTasks_AppCrisisCommittees_CrisisCommitteeId",
                        column: x => x.CrisisCommitteeId,
                        principalTable: "AppCrisisCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeActions_CrisisCommitteeId",
                table: "AppCrisisCommitteeActions",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeAgreements_CrisisCommitteeId",
                table: "AppCrisisCommitteeAgreements",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeChannels_CrisisCommitteeId",
                table: "AppCrisisCommitteeChannels",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeMessage_CrisisCommitteeId",
                table: "AppCrisisCommitteeMessage",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteePlans_CrisisCommitteeId",
                table: "AppCrisisCommitteePlans",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeSectors_CrisisCommitteeId",
                table: "AppCrisisCommitteeSectors",
                column: "CrisisCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeSectors_DirectoryGovernmentId",
                table: "AppCrisisCommitteeSectors",
                column: "DirectoryGovernmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrisisCommitteeTasks_CrisisCommitteeId",
                table: "AppCrisisCommitteeTasks",
                column: "CrisisCommitteeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeActions");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeAgreements");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeChannels");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeMessage");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteePlans");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeSectors");

            migrationBuilder.DropTable(
                name: "AppCrisisCommitteeTasks");
        }
    }
}
