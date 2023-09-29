using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class fixresponsiblecompromises : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCompromiseInvolved",
                table: "AppCompromiseInvolved");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleActorId",
                table: "AppCompromiseInvolved",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CompromiseId",
                table: "AppCompromiseInvolved",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AppCompromiseInvolved",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleSubActorId",
                table: "AppCompromiseInvolved",
                type: "INT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCompromiseInvolved",
                table: "AppCompromiseInvolved",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseInvolved_CompromiseId",
                table: "AppCompromiseInvolved",
                column: "CompromiseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompromiseInvolved_ResponsibleSubActorId",
                table: "AppCompromiseInvolved",
                column: "ResponsibleSubActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCompromiseInvolved_AppResponsibleSubActors_ResponsibleSubActorId",
                table: "AppCompromiseInvolved",
                column: "ResponsibleSubActorId",
                principalTable: "AppResponsibleSubActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCompromiseInvolved_AppResponsibleSubActors_ResponsibleSubActorId",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppCompromiseInvolved",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromiseInvolved_CompromiseId",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropIndex(
                name: "IX_AppCompromiseInvolved_ResponsibleSubActorId",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AppCompromiseInvolved");

            migrationBuilder.DropColumn(
                name: "ResponsibleSubActorId",
                table: "AppCompromiseInvolved");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleActorId",
                table: "AppCompromiseInvolved",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AlterColumn<long>(
                name: "CompromiseId",
                table: "AppCompromiseInvolved",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppCompromiseInvolved",
                table: "AppCompromiseInvolved",
                columns: new[] { "CompromiseId", "ResponsibleActorId" });
        }
    }
}
