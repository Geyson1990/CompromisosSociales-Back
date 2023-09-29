using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictViolenceFacts_AppPersons_ManagerId",
                table: "AppSocialConflictViolenceFacts");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "AppSocialConflictViolenceFacts",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AppSocialConflictViolenceFactLocations",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubication",
                table: "AppSocialConflictViolenceFactLocations",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictViolenceFacts_AppPersons_ManagerId",
                table: "AppSocialConflictViolenceFacts",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictViolenceFacts_AppPersons_ManagerId",
                table: "AppSocialConflictViolenceFacts");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.DropColumn(
                name: "Ubication",
                table: "AppSocialConflictViolenceFactLocations");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "AppSocialConflictViolenceFacts",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictViolenceFacts_AppPersons_ManagerId",
                table: "AppSocialConflictViolenceFacts",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
