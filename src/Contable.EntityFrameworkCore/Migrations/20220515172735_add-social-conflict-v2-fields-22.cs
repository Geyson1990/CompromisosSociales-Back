using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors");

            migrationBuilder.AlterColumn<int>(
                name: "ActorMovementId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INT");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors",
                column: "ActorMovementId",
                principalTable: "AppActorMovements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors");

            migrationBuilder.AlterColumn<int>(
                name: "ActorMovementId",
                table: "AppSocialConflictActors",
                type: "INT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors",
                column: "ActorMovementId",
                principalTable: "AppActorMovements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
