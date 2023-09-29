using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertaditionalinformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Actions",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AditionalInformation",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertDemandId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlertResponsibleId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalystId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Demand",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recommendations",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "AppSocialConflictAlerts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubTypologyId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypologyId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppAlertDemands",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlertDemands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppAlertResponsibles",
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
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAlertResponsibles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_AlertDemandId",
                table: "AppSocialConflictAlerts",
                column: "AlertDemandId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_AlertResponsibleId",
                table: "AppSocialConflictAlerts",
                column: "AlertResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_AnalystId",
                table: "AppSocialConflictAlerts",
                column: "AnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_CoordinatorId",
                table: "AppSocialConflictAlerts",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_ManagerId",
                table: "AppSocialConflictAlerts",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_SubTypologyId",
                table: "AppSocialConflictAlerts",
                column: "SubTypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictAlerts_TypologyId",
                table: "AppSocialConflictAlerts",
                column: "TypologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppAlertDemands_AlertDemandId",
                table: "AppSocialConflictAlerts",
                column: "AlertDemandId",
                principalTable: "AppAlertDemands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppAlertResponsibles_AlertResponsibleId",
                table: "AppSocialConflictAlerts",
                column: "AlertResponsibleId",
                principalTable: "AppAlertResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_AnalystId",
                table: "AppSocialConflictAlerts",
                column: "AnalystId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_CoordinatorId",
                table: "AppSocialConflictAlerts",
                column: "CoordinatorId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_ManagerId",
                table: "AppSocialConflictAlerts",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflictAlerts",
                column: "SubTypologyId",
                principalTable: "AppSubTypologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppTypologies_TypologyId",
                table: "AppSocialConflictAlerts",
                column: "TypologyId",
                principalTable: "AppTypologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppAlertDemands_AlertDemandId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppAlertResponsibles_AlertResponsibleId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_AnalystId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_CoordinatorId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppPersons_ManagerId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppTypologies_TypologyId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropTable(
                name: "AppAlertDemands");

            migrationBuilder.DropTable(
                name: "AppAlertResponsibles");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_AlertDemandId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_AlertResponsibleId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_AnalystId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_CoordinatorId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_ManagerId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_SubTypologyId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictAlerts_TypologyId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Actions",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "AditionalInformation",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "AlertDemandId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "AlertResponsibleId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "AnalystId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Demand",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "SubTypologyId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.DropColumn(
                name: "TypologyId",
                table: "AppSocialConflictAlerts");
        }
    }
}
