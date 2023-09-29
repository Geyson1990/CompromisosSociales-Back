using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addresponsibletypesandcompromiselabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompromiseLabelId",
                table: "AppCompromises",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WomanCompromise",
                table: "AppCompromises",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppCompromiseLabels",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCompromiseLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppResponsibleTypes",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResponsibleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppResponsibleSubTypes",
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
                    ResponsibleTypeId = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppResponsibleSubTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppResponsibleSubTypes_AppResponsibleTypes_ResponsibleTypeId",
                        column: x => x.ResponsibleTypeId,
                        principalTable: "AppResponsibleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_CompromiseLabelId",
                table: "AppCompromises",
                column: "CompromiseLabelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppResponsibleSubTypes_ResponsibleTypeId",
                table: "AppResponsibleSubTypes",
                column: "ResponsibleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppCompromiseLabels_CompromiseLabelId",
                table: "AppCompromises",
                column: "CompromiseLabelId",
                principalTable: "AppCompromiseLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppCompromiseLabels_CompromiseLabelId",
                table: "AppCompromises");

            migrationBuilder.DropTable(
                name: "AppCompromiseLabels");

            migrationBuilder.DropTable(
                name: "AppResponsibleSubTypes");

            migrationBuilder.DropTable(
                name: "AppResponsibleTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_CompromiseLabelId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "CompromiseLabelId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "WomanCompromise",
                table: "AppCompromises");
        }
    }
}
