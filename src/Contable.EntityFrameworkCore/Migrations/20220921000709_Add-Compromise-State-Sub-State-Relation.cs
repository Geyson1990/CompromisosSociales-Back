using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AddCompromiseStateSubStateRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompromiseStateId",
                table: "AppCompromises",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompromiseSubStateId",
                table: "AppCompromises",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_CompromiseStateId",
                table: "AppCompromises",
                column: "CompromiseStateId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_CompromiseSubStateId",
                table: "AppCompromises",
                column: "CompromiseSubStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppCompromiseStates_CompromiseStateId",
                table: "AppCompromises",
                column: "CompromiseStateId",
                principalTable: "AppCompromiseStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppCompromiseSubStates_CompromiseSubStateId",
                table: "AppCompromises",
                column: "CompromiseSubStateId",
                principalTable: "AppCompromiseSubStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppCompromiseStates_CompromiseStateId",
                table: "AppCompromises");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppCompromiseSubStates_CompromiseSubStateId",
                table: "AppCompromises");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_CompromiseStateId",
                table: "AppCompromises");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_CompromiseSubStateId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "CompromiseStateId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "CompromiseSubStateId",
                table: "AppCompromises");
        }
    }
}
