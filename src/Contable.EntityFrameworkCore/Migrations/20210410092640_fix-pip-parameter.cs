using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixpipparameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateMEF",
                table: "AppPIPMEF",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "AppParameter",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateMEF",
                table: "AppPIPMEF");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "AppParameter");
        }
    }
}
