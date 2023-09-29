using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class socialconflicteditfieldlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictViolenceFacts",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actions",
                table: "AppSocialConflictViolenceFacts",
                type: "VARCHAR(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "AppSocialConflictStates",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictStates",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Strategy",
                table: "AppSocialConflicts",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "AppSocialConflicts",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plaint",
                table: "AppSocialConflicts",
                type: "VARCHAR(6000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FactorContext",
                table: "AppSocialConflicts",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictManagements",
                type: "VARCHAR(5000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictGeneralFacts",
                type: "VARCHAR(6000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictViolenceFacts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Actions",
                table: "AppSocialConflictViolenceFacts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "AppSocialConflictStates",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictStates",
                type: "VARCHAR(3000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Strategy",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plaint",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(6000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FactorContext",
                table: "AppSocialConflicts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictManagements",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(5000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AppSocialConflictGeneralFacts",
                type: "VARCHAR(1000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(6000)",
                oldNullable: true);
        }
    }
}
