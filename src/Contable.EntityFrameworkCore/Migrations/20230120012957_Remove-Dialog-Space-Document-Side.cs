using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class RemoveDialogSpaceDocumentSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Side",
                table: "AppDialogSpaceDocuments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }
    }
}
