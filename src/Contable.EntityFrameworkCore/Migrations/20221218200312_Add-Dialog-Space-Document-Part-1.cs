using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceDocumentPart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasInstallation",
                table: "AppDialogSpaceDocuments",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Range",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RangeSide",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "HasInstallation",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "RangeSide",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "AppDialogSpaceDocuments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppDialogSpaceDocuments");
        }
    }
}
