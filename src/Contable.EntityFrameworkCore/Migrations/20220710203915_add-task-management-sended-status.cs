using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addtaskmanagementsendedstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsible",
                table: "AppTaskManagement");

            migrationBuilder.AddColumn<bool>(
                name: "Sended",
                table: "AppTaskManagement",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sended",
                table: "AppSocialConflictTaskManagements",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sended",
                table: "AppTaskManagement");

            migrationBuilder.DropColumn(
                name: "Sended",
                table: "AppSocialConflictTaskManagements");

            migrationBuilder.AddColumn<string>(
                name: "Responsible",
                table: "AppTaskManagement",
                type: "VARCHAR(100)",
                nullable: true);
        }
    }
}
