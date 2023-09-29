using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addreportserverinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictSugerences",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AppReports",
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
                    Description = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppReports");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictSugerences",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);
        }
    }
}
