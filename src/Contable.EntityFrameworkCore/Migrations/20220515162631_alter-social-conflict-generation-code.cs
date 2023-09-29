using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class altersocialconflictgenerationcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialConflictStates_AppPersons_ManagerId",
                table: "SocialConflictStates");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialConflictStates_AppSocialConflicts_SocialConflictId",
                table: "SocialConflictStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialConflictStates",
                table: "SocialConflictStates");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AppSocialConflicts");

            migrationBuilder.RenameTable(
                name: "SocialConflictStates",
                newName: "AppSocialConflictStates");

            migrationBuilder.RenameIndex(
                name: "IX_SocialConflictStates_SocialConflictId",
                table: "AppSocialConflictStates",
                newName: "IX_AppSocialConflictStates_SocialConflictId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialConflictStates_ManagerId",
                table: "AppSocialConflictStates",
                newName: "IX_AppSocialConflictStates_ManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSocialConflictStates",
                table: "AppSocialConflictStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictStates_AppPersons_ManagerId",
                table: "AppSocialConflictStates",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictStates_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictStates",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictStates_AppPersons_ManagerId",
                table: "AppSocialConflictStates");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictStates_AppSocialConflicts_SocialConflictId",
                table: "AppSocialConflictStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSocialConflictStates",
                table: "AppSocialConflictStates");

            migrationBuilder.RenameTable(
                name: "AppSocialConflictStates",
                newName: "SocialConflictStates");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictStates_SocialConflictId",
                table: "SocialConflictStates",
                newName: "IX_SocialConflictStates_SocialConflictId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictStates_ManagerId",
                table: "SocialConflictStates",
                newName: "IX_SocialConflictStates_ManagerId");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialConflictStates",
                table: "SocialConflictStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialConflictStates_AppPersons_ManagerId",
                table: "SocialConflictStates",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialConflictStates_AppSocialConflicts_SocialConflictId",
                table: "SocialConflictStates",
                column: "SocialConflictId",
                principalTable: "AppSocialConflicts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
