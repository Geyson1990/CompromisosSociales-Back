using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictActors",
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
                    SocialConflictId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ActorId = table.Column<int>(type: "INT", nullable: false),
                    SubActorId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictActors_AppActors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AppActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictActors_AppSocialConflicts_SocialConflictId",
                        column: x => x.SocialConflictId,
                        principalTable: "AppSocialConflicts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictActors_AppSubActors_SubActorId",
                        column: x => x.SubActorId,
                        principalTable: "AppSubActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictActors_ActorId",
                table: "AppSocialConflictActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictActors_SocialConflictId",
                table: "AppSocialConflictActors",
                column: "SocialConflictId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictActors_SubActorId",
                table: "AppSocialConflictActors",
                column: "SubActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictActors");
        }
    }
}
