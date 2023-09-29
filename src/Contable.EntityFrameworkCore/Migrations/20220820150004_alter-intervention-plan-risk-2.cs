using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alterinterventionplanrisk2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AppInterventionPlanRisks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AppInterventionPlanRisks",
                type: "VARCHAR(5000)",
                nullable: true);
        }
    }
}
