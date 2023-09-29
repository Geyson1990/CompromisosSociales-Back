using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Dialog",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnalistId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSectorId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubTypologyId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypologyId",
                table: "AppSocialConflicts",
                type: "INT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_AnalistId",
                table: "AppSocialConflicts",
                column: "AnalistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_CoordinatorId",
                table: "AppSocialConflicts",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_ManagerId",
                table: "AppSocialConflicts",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_SectorId",
                table: "AppSocialConflicts",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_SubSectorId",
                table: "AppSocialConflicts",
                column: "SubSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_SubTypologyId",
                table: "AppSocialConflicts",
                column: "SubTypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSocialConflicts_TypologyId",
                table: "AppSocialConflicts",
                column: "TypologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalistId",
                table: "AppSocialConflicts",
                column: "AnalistId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_CoordinatorId",
                table: "AppSocialConflicts",
                column: "CoordinatorId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_ManagerId",
                table: "AppSocialConflicts",
                column: "ManagerId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppSectors_SectorId",
                table: "AppSocialConflicts",
                column: "SectorId",
                principalTable: "AppSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppSubSectors_SubSectorId",
                table: "AppSocialConflicts",
                column: "SubSectorId",
                principalTable: "AppSubSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflicts",
                column: "SubTypologyId",
                principalTable: "AppSubTypologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflicts_AppTypologies_TypologyId",
                table: "AppSocialConflicts",
                column: "TypologyId",
                principalTable: "AppTypologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_AnalistId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_CoordinatorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppPersons_ManagerId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppSectors_SectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppSubSectors_SubSectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppSubTypologies_SubTypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflicts_AppTypologies_TypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_AnalistId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_CoordinatorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_ManagerId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_SectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_SubSectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_SubTypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.DropIndex(
                name: "IX_AppSocialConflicts_TypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "AnalistId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "CoordinatorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "SubSectorId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "SubTypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.DropColumn(
                name: "TypologyId",
                table: "AppSocialConflicts");

            migrationBuilder.AlterColumn<string>(
                name: "Dialog",
                table: "AppSocialConflicts",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);
        }
    }
}
