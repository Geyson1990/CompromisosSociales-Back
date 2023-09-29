using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class FixCompromiseRecordRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RecordId",
                table: "AppCompromises",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RecordId",
                table: "AppCompromises",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }
    }
}
