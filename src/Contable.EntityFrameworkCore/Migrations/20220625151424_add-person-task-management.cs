using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addpersontaskmanagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTaskManagementPersons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskManagementId = table.Column<long>(type: "BIGINT", nullable: false),
                    PersonId = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTaskManagementPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTaskManagementPersons_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppTaskManagementPersons_AppTaskManagement_TaskManagementId",
                        column: x => x.TaskManagementId,
                        principalTable: "AppTaskManagement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTaskManagementPersons_PersonId",
                table: "AppTaskManagementPersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTaskManagementPersons_TaskManagementId",
                table: "AppTaskManagementPersons",
                column: "TaskManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTaskManagementPersons");
        }
    }
}
