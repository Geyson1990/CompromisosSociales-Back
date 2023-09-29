using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addprojectriskweightdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ImpactWeight",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProbabilityWeight",
                table: "AppProjectRisks",
                type: "DECIMAL(27,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpactWeight",
                table: "AppProjectRisks");

            migrationBuilder.DropColumn(
                name: "ProbabilityWeight",
                table: "AppProjectRisks");
        }
    }
}
