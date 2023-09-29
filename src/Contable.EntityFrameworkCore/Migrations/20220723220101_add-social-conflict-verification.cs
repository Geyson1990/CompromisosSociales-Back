using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictStates",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CaseNameVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConditionVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DescriptionVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManagementVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProblemVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RiskVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StateVerification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflicts",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictRisks",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictManagements",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictConditions",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictStates");

            migrationBuilder.DropColumn(
                name: "CaseNameVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ConditionVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "DescriptionVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ManagementVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ProblemVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "RiskVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "StateVerification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictRisks");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictManagements");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictConditions");
        }
    }
}
