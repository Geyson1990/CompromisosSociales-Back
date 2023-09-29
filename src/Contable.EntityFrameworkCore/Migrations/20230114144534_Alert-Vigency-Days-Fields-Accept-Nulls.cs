using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlertVigencyDaysFieldsAcceptNulls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VigencyDays",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VigencyDays",
                table: "AppDialogSpaceDocuments",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);
        }
    }
}
