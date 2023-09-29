using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class socialconflictalertpositioninterestlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "AppSocialConflictActors",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interest",
                table: "AppSocialConflictActors",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Interest",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);
        }
    }
}
