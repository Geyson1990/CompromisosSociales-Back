using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixsocialconflicttask2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppSocialConflictTaskManagementComments",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppSocialConflictTaskManagementComments");

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "AppSocialConflictTaskManagementComments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppSocialConflictTaskManagementComments",
                table: "AppSocialConflictTaskManagementComments",
                column: "TestId");
        }
    }
}
