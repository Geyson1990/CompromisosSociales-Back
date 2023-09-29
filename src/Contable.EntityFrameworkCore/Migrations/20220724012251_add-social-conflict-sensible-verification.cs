using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictsensibleverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictSensibleStates",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CaseNameVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConditionVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManagementVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProblemVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RiskVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StateVerification",
                table: "AppSocialConflictSensibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Verification",
                table: "AppSocialConflictSensibles",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictSensibleRisks",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictSensibleManagements",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "AppSocialConflictSensibleConditions",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppSocialConflictSensibleVerificationHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    SocialConflictSensibleId = table.Column<int>(type: "INT", nullable: false),
                    Site = table.Column<int>(type: "INT", nullable: false),
                    OldState = table.Column<bool>(type: "BIT", nullable: false),
                    NewState = table.Column<bool>(type: "BIT", nullable: false),
                    EntityId = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSocialConflictSensibleVerificationHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSocialConflictSensibleVerificationHistories_AppSocialConflictSensibles_SocialConflictSensibleId",
                        column: x => x.SocialConflictSensibleId,
                        principalTable: "AppSocialConflictSensibles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictSensibleVerificationHistories_SocialConflictSensibleId",
                table: "AppSocialConflictSensibleVerificationHistories",
                column: "SocialConflictSensibleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSocialConflictSensibleVerificationHistories");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictSensibleStates");

            migrationBuilder.DropColumn(
                name: "CaseNameVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "ConditionVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "ManagementVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "ProblemVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "RiskVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "StateVerification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictSensibles");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictSensibleRisks");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictSensibleManagements");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "AppSocialConflictSensibleConditions");
        }
    }
}
