using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class adddirectorygovernmenttypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppDirectoryGovernmentTypes",
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
                    table.PrimaryKey("PK_AppDirectoryGovernmentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDirectoryGovernments_DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments",
                column: "DirectoryGovernmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppDirectoryGovernments_AppDirectoryGovernmentTypes_DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments",
                column: "DirectoryGovernmentTypeId",
                principalTable: "AppDirectoryGovernmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppDirectoryGovernments_AppDirectoryGovernmentTypes_DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments");

            migrationBuilder.DropTable(
                name: "AppDirectoryGovernmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_AppDirectoryGovernments_DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments");

            migrationBuilder.DropColumn(
                name: "DirectoryGovernmentTypeId",
                table: "AppDirectoryGovernments");
        }
    }
}
