using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FactorContext",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plaint",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceContext",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RiskId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strategy",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_RiskId",
                table: "AppSocialConflicts",
                column: "RiskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppRisks_RiskId",
                table: "AppSocialConflicts",
                column: "RiskId",
                principalTable: "AppRisks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppRisks_RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "FactorContext",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Plaint",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ResourceContext",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "RiskId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Strategy",
                table: "AppSocialConflicts");
        }
    }
}
