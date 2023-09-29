using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddSectorMeetOptionalSocialConflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeets_AppSocialConflicts_SocialConflictId",
                table: "AppSectorMeets");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSectorMeets",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeets_AppSocialConflicts_SocialConflictId",
                table: "AppSectorMeets",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSectorMeets_AppSocialConflicts_SocialConflictId",
                table: "AppSectorMeets");

            migrationBuilder.AlterColumn<int>(
                name: "SocialConflictId",
                table: "AppSectorMeets",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSectorMeets_AppSocialConflicts_SocialConflictId",
                table: "AppSectorMeets",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
