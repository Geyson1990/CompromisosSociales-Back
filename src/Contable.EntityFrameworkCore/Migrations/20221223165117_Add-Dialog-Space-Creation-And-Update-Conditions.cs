using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddDialogSpaceCreationAndUpdateConditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RangeSide",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<int>(
                name: "Days",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RangeSide",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Days",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
