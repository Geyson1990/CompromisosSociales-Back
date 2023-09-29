using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addresponsibleactortypesrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsibleSubTypeId",
                table: "AppResponsibleActors",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleTypeId",
                table: "AppResponsibleActors",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppResponsibleActors_ResponsibleSubTypeId",
                table: "AppResponsibleActors",
                column: "ResponsibleSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppResponsibleActors_ResponsibleTypeId",
                table: "AppResponsibleActors",
                column: "ResponsibleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppResponsibleActors_AppResponsibleSubTypes_ResponsibleSubTypeId",
                table: "AppResponsibleActors",
                column: "ResponsibleSubTypeId",
                principalTable: "AppResponsibleSubTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppResponsibleActors_AppResponsibleTypes_ResponsibleTypeId",
                table: "AppResponsibleActors",
                column: "ResponsibleTypeId",
                principalTable: "AppResponsibleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppResponsibleActors_AppResponsibleSubTypes_ResponsibleSubTypeId",
                table: "AppResponsibleActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppResponsibleActors_AppResponsibleTypes_ResponsibleTypeId",
                table: "AppResponsibleActors");

            migrationBuilder.DropIndex(
                name: "IX_AppResponsibleActors_ResponsibleSubTypeId",
                table: "AppResponsibleActors");

            migrationBuilder.DropIndex(
                name: "IX_AppResponsibleActors_ResponsibleTypeId",
                table: "AppResponsibleActors");

            migrationBuilder.DropColumn(
                name: "ResponsibleSubTypeId",
                table: "AppResponsibleActors");

            migrationBuilder.DropColumn(
                name: "ResponsibleTypeId",
                table: "AppResponsibleActors");
        }
    }
}
