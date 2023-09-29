using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddQuizComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppQuizCompletes",
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
                    Type = table.Column<int>(type: "INT", nullable: false),
                    QuizStateId = table.Column<int>(type: "INT", nullable: true),
                    AdminitrativeId = table.Column<long>(type: "BIGINT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuizCompletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuizCompletes_AbpUsers_AdminitrativeId",
                        column: x => x.AdminitrativeId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppQuizCompletes_QuizStates_QuizStateId",
                        column: x => x.QuizStateId,
                        principalTable: "QuizStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppQuizCompleteForms",
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
                    QuizCompleteId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(2500)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Required = table.Column<bool>(type: "BIT", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuizCompleteForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuizCompleteForms_AppQuizCompletes_QuizCompleteId",
                        column: x => x.QuizCompleteId,
                        principalTable: "AppQuizCompletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppQuizCompleteResources",
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
                    QuizCompleteId = table.Column<int>(type: "INT", nullable: false),
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
                    table.PrimaryKey("PK_AppQuizCompleteResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuizCompleteResources_AppQuizCompletes_QuizCompleteId",
                        column: x => x.QuizCompleteId,
                        principalTable: "AppQuizCompletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppQuizCompleteFormOptions",
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
                    QuizCompleteFormId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(2500)", nullable: true),
                    Index = table.Column<int>(type: "INT", nullable: false),
                    Extra = table.Column<int>(type: "INT", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(3000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppQuizCompleteFormOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppQuizCompleteFormOptions_AppQuizCompleteForms_QuizCompleteFormId",
                        column: x => x.QuizCompleteFormId,
                        principalTable: "AppQuizCompleteForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppQuizCompleteFormOptions_QuizCompleteFormId",
                table: "AppQuizCompleteFormOptions",
                column: "QuizCompleteFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AppQuizCompleteForms_QuizCompleteId",
                table: "AppQuizCompleteForms",
                column: "QuizCompleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AppQuizCompleteResources_QuizCompleteId",
                table: "AppQuizCompleteResources",
                column: "QuizCompleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AppQuizCompletes_AdminitrativeId",
                table: "AppQuizCompletes",
                column: "AdminitrativeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppQuizCompletes_QuizStateId",
                table: "AppQuizCompletes",
                column: "QuizStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppQuizCompleteFormOptions");

            migrationBuilder.DropTable(
                name: "AppQuizCompleteResources");

            migrationBuilder.DropTable(
                name: "AppQuizCompleteForms");

            migrationBuilder.DropTable(
                name: "AppQuizCompletes");
        }
    }
}
