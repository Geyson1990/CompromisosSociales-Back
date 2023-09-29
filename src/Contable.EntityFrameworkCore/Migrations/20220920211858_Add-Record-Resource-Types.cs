using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddRecordResourceTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecordResourceTypeId",
                table: "AppRecordResources",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppRecordResourceTypes",
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
                    table.PrimaryKey("PK_AppRecordResourceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRecordResources_RecordResourceTypeId",
                table: "AppRecordResources",
                column: "RecordResourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRecordResources_AppRecordResourceTypes_RecordResourceTypeId",
                table: "AppRecordResources",
                column: "RecordResourceTypeId",
                principalTable: "AppRecordResourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRecordResources_AppRecordResourceTypes_RecordResourceTypeId",
                table: "AppRecordResources");

            migrationBuilder.DropTable(
                name: "AppRecordResourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppRecordResources_RecordResourceTypeId",
                table: "AppRecordResources");

            migrationBuilder.DropColumn(
                name: "RecordResourceTypeId",
                table: "AppRecordResources");
        }
    }
}
