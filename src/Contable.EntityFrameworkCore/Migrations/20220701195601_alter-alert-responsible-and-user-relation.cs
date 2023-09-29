using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class alteralertresponsibleanduserrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Enabled",
                table: "AppAlertResponsibles",
                type: "BIT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)");

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "AppAlertResponsibles",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Tracing",
                table: "AppAlertResponsibles",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AlertResponsibleId",
                table: "AbpUsers",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_AlertResponsibleId",
                table: "AbpUsers",
                column: "AlertResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppAlertResponsibles_AlertResponsibleId",
                table: "AbpUsers",
                column: "AlertResponsibleId",
                principalTable: "AppAlertResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppAlertResponsibles_AlertResponsibleId",
                table: "AbpUsers");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_AlertResponsibleId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "AppAlertResponsibles");

            migrationBuilder.DropColumn(
                name: "Tracing",
                table: "AppAlertResponsibles");

            migrationBuilder.DropColumn(
                name: "AlertResponsibleId",
                table: "AbpUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Enabled",
                table: "AppAlertResponsibles",
                type: "VARCHAR(255)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BIT");
        }
    }
}
