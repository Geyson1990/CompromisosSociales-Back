using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictalertactors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictActors");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<int>(
                name: "Site",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SocialConflictAlertId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflictActors_SocialConflictAlertId",
                table: "AppSocialConflictActors",
                column: "SocialConflictAlertId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflictAlerts_SocialConflictAlertId",
                table: "AppSocialConflictActors",
                column: "SocialConflictAlertId",
                principalTable: "AppSocialConflictAlerts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictActors",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflictAlerts_SocialConflictAlertId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflictActors_SocialConflictAlertId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "SocialConflictAlertId",
                table: "AppSocialConflictActors");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictActors",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
