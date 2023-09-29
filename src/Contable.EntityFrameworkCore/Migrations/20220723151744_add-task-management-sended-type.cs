using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addtaskmanagementsendedtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sended",
                table: "AppTaskManagement",
                newName: "SendedDeadline");

            migrationBuilder.RenameColumn(
                name: "Sended",
                table: "AppSocialConflictTaskManagements",
                newName: "SendedDeadline");

            migrationBuilder.AddColumn<bool>(
                name: "SendedCreation",
                table: "AppTaskManagement",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendedCreation",
                table: "AppSocialConflictTaskManagements",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendedCreation",
                table: "AppTaskManagement");

            migrationBuilder.DropColumn(
                name: "SendedCreation",
                table: "AppSocialConflictTaskManagements");

            migrationBuilder.RenameColumn(
                name: "SendedDeadline",
                table: "AppTaskManagement",
                newName: "Sended");

            migrationBuilder.RenameColumn(
                name: "SendedDeadline",
                table: "AppSocialConflictTaskManagements",
                newName: "Sended");
        }
    }
}
