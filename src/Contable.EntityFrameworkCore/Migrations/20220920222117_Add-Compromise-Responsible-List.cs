using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddCompromiseResponsibleList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppRecordResources");

            migrationBuilder.CreateTable(
                name: "AppCompromiseResponsibles",
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
                    ResponsibleActorId = table.Column<int>(type: "INT", nullable: false),
                    ResponsibleSubActorId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCompromiseResponsibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCompromiseResponsibles_AppCompromises_CompromiseId",
                        column: x => x.CompromiseId,
                        principalTable: "AppCompromises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCompromiseResponsibles_AppResponsibleActors_ResponsibleActorId",
                        column: x => x.ResponsibleActorId,
                        principalTable: "AppResponsibleActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppCompromiseResponsibles_AppResponsibleSubActors_ResponsibleSubActorId",
                        column: x => x.ResponsibleSubActorId,
                        principalTable: "AppResponsibleSubActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseResponsibles_CompromiseId",
                table: "AppCompromiseResponsibles",
                column: "CompromiseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseResponsibles_ResponsibleActorId",
                table: "AppCompromiseResponsibles",
                column: "ResponsibleActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseResponsibles_ResponsibleSubActorId",
                table: "AppCompromiseResponsibles",
                column: "ResponsibleSubActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCompromiseResponsibles");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppRecordResources",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }
    }
}
