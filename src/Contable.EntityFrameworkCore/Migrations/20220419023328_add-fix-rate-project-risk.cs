using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addfixrateprojectrisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FixImpactRate",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FixProbabilityRate",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Impact",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Probability",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixImpactRate",
                table: "AppProjectRisks");

            migrationBuilder.DropColumn(
                name: "FixProbabilityRate",
                table: "AppProjectRisks");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "AppProjectRisks");

            migrationBuilder.DropColumn(
                name: "Probability",
                table: "AppProjectRisks");
        }
    }
}
