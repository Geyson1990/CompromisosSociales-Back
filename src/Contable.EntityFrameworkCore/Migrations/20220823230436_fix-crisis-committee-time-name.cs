using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixcrisiscommitteetimename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrisisComitteTime",
                table: "AppCrisisCommittees",
                newName: "CrisisCommitteeTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrisisCommitteeTime",
                table: "AppCrisisCommittees",
                newName: "CrisisComitteTime");
        }
    }
}
