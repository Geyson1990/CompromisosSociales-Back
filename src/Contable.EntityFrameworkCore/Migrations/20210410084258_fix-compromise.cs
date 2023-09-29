using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixcompromise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppResponsibleActors_InvolvedId",
                table: "AppCompromises");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppResponsibleSubActors_ResponsibleId",
                table: "AppCompromises");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_InvolvedId",
                table: "AppCompromises");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_ResponsibleId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "InvolvedId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "AppCompromises");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleActorId",
                table: "AppCompromises",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleSubActorId",
                table: "AppCompromises",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppCompromiseInvolved",
                columns: table => new
                {
                    CompromiseId = table.Column<long>(nullable: false),
                    ResponsibleActorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCompromiseInvolved", x => new { x.CompromiseId, x.ResponsibleActorId });
                    table.ForeignKey(
                        name: "FK_AppCompromiseInvolved_AppCompromises_CompromiseId",
                        column: x => x.CompromiseId,
                        principalTable: "AppCompromises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCompromiseInvolved_AppResponsibleActors_ResponsibleActorId",
                        column: x => x.ResponsibleActorId,
                        principalTable: "AppResponsibleActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_ResponsibleActorId",
                table: "AppCompromises",
                column: "ResponsibleActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_ResponsibleSubActorId",
                table: "AppCompromises",
                column: "ResponsibleSubActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseInvolved_ResponsibleActorId",
                table: "AppCompromiseInvolved",
                column: "ResponsibleActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppResponsibleActors_ResponsibleActorId",
                table: "AppCompromises",
                column: "ResponsibleActorId",
                principalTable: "AppResponsibleActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppResponsibleSubActors_ResponsibleSubActorId",
                table: "AppCompromises",
                column: "ResponsibleSubActorId",
                principalTable: "AppResponsibleSubActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppResponsibleActors_ResponsibleActorId",
                table: "AppCompromises");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromises_AppResponsibleSubActors_ResponsibleSubActorId",
                table: "AppCompromises");

            migrationBuilder.DropTable(
                name: "AppCompromiseInvolved");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_ResponsibleActorId",
                table: "AppCompromises");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromises_ResponsibleSubActorId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "ResponsibleActorId",
                table: "AppCompromises");

            migrationBuilder.DropColumn(
                name: "ResponsibleSubActorId",
                table: "AppCompromises");

            migrationBuilder.AddColumn<int>(
                name: "InvolvedId",
                table: "AppCompromises",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleId",
                table: "AppCompromises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_InvolvedId",
                table: "AppCompromises",
                column: "InvolvedId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromises_ResponsibleId",
                table: "AppCompromises",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppResponsibleActors_InvolvedId",
                table: "AppCompromises",
                column: "InvolvedId",
                principalTable: "AppResponsibleActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromises_AppResponsibleSubActors_ResponsibleId",
                table: "AppCompromises",
                column: "ResponsibleId",
                principalTable: "AppResponsibleSubActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
