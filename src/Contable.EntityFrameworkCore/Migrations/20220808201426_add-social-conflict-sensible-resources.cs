using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsensibleresources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleResources",
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
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    CommonFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ResourceFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    SectionFolder = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    FileName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Size = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Extension = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    ClassName = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Resource = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleResources_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleResources_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleResources",
                column: "SocialConflictSensibleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleResources");
        }
    }
}
