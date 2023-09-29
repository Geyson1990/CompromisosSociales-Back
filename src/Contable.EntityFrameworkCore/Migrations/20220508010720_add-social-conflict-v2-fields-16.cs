using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Other",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ResourceContext",
                table: "AppSocialConflicts");

            migrationBuilder.AlterColumn<string>(
                name: "Community",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceContext",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Community",
                table: "AppSocialConflictActors",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);
        }
    }
}
