using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addfixpendingparameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "AppRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PIM",
                table: "AppPIPMEF",
                type: "NUMERIC(27,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PIA",
                table: "AppPIPMEF",
                type: "NUMERIC(27,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccumulatedAccrued",
                table: "AppPIPMEF",
                type: "NUMERIC(27,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Accrued",
                table: "AppPIPMEF",
                type: "NUMERIC(27,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AppPIPMEF",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UpdatedCost",
                table: "AppPIPMEF",
                type: "NUMERIC(27,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Step",
                table: "AppParameter",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppParameter",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EntityDtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityDtos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityDtos_Id",
                table: "EntityDtos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityDtos");

            migrationBuilder.DropColumn(
                name: "Filter",
                table: "AppRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppPIPMEF");

            migrationBuilder.DropColumn(
                name: "UpdatedCost",
                table: "AppPIPMEF");

            migrationBuilder.DropColumn(
                name: "Step",
                table: "AppParameter");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppParameter");

            migrationBuilder.AlterColumn<decimal>(
                name: "PIM",
                table: "AppPIPMEF",
                type: "NUMERIC(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(27,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PIA",
                table: "AppPIPMEF",
                type: "VARCHAR(50)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(27,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccumulatedAccrued",
                table: "AppPIPMEF",
                type: "NUMERIC(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(27,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Accrued",
                table: "AppPIPMEF",
                type: "NUMERIC(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "NUMERIC(27,2)");
        }
    }
}
