using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class AlterQuizStatesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppQuizCompletes_QuizStates_QuizStateId",
                table: "AppQuizCompletes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizStates",
                table: "QuizStates");

            migrationBuilder.RenameTable(
                name: "QuizStates",
                newName: "AppQuizStates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppQuizStates",
                table: "AppQuizStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppQuizCompletes_AppQuizStates_QuizStateId",
                table: "AppQuizCompletes",
                column: "QuizStateId",
                principalTable: "AppQuizStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppQuizCompletes_AppQuizStates_QuizStateId",
                table: "AppQuizCompletes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppQuizStates",
                table: "AppQuizStates");

            migrationBuilder.RenameTable(
                name: "AppQuizStates",
                newName: "QuizStates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizStates",
                table: "QuizStates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppQuizCompletes_QuizStates_QuizStateId",
                table: "AppQuizCompletes",
                column: "QuizStateId",
                principalTable: "QuizStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
