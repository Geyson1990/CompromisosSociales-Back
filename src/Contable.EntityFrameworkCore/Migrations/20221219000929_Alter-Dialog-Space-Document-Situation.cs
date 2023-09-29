using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlterDialogSpaceDocumentSituation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Installation",
                table: "AppDialogSpaceDocumentSituations",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Vigency",
                table: "AppDialogSpaceDocumentSituations",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Installation",
                table: "AppDialogSpaceDocumentSituations");

            migrationBuilder.DropColumn(
                name: "Vigency",
                table: "AppDialogSpaceDocumentSituations");
        }
    }
}
