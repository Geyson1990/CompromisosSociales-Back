using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertalternativesocialconflictadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictAlerts",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictAlerts");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSocialConflictAlerts",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictAlerts_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictAlerts",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
