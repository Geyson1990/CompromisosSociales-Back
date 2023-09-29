using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Contable.Migrations
{
    public partial class addsocialconflictv2fields13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppActors_ActorId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppSubActors_SubActorId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropTable(
                name: "AppSubActors");

            migrationBuilder.DropTable(
                name: "AppActors");

            migrationBuilder.RenameColumn(
                name: "SubActorId",
                table: "AppSocialConflictActors",
                newName: "ActorTypeId");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "AppSocialConflictActors",
                newName: "ActorMovementId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictActors_SubActorId",
                table: "AppSocialConflictActors",
                newName: "IX_AppSocialConflictActors_ActorTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictActors_ActorId",
                table: "AppSocialConflictActors",
                newName: "IX_AppSocialConflictActors_ActorMovementId");

            migrationBuilder.AddColumn<string>(
                name: "Community",
                table: "AppSocialConflictActors",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "AppSocialConflictActors",
                type: "VARCHAR(32)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "AppSocialConflictActors",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interest",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoliticalAssociation",
                table: "AppSocialConflictActors",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AppSocialConflictActors",
                type: "VARCHAR(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoliticalAssociation",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "AppSocialConflictActors",
                type: "VARCHAR(500)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppActorMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppActorMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppActorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    ShowDetail = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppActorTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors",
                column: "ActorMovementId",
                principalTable: "AppActorMovements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppActorTypes_ActorTypeId",
                table: "AppSocialConflictActors",
                column: "ActorTypeId",
                principalTable: "AppActorTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppActorMovements_ActorMovementId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppSocialConflictActors_AppActorTypes_ActorTypeId",
                table: "AppSocialConflictActors");

            migrationBuilder.DropTable(
                name: "AppActorMovements");

            migrationBuilder.DropTable(
                name: "AppActorTypes");

            migrationBuilder.DropColumn(
                name: "Community",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "IsPoliticalAssociation",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "PoliticalAssociation",
                table: "AppSocialConflictActors");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AppSocialConflictActors");

            migrationBuilder.RenameColumn(
                name: "ActorTypeId",
                table: "AppSocialConflictActors",
                newName: "SubActorId");

            migrationBuilder.RenameColumn(
                name: "ActorMovementId",
                table: "AppSocialConflictActors",
                newName: "ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictActors_ActorTypeId",
                table: "AppSocialConflictActors",
                newName: "IX_AppSocialConflictActors_SubActorId");

            migrationBuilder.RenameIndex(
                name: "IX_AppSocialConflictActors_ActorMovementId",
                table: "AppSocialConflictActors",
                newName: "IX_AppSocialConflictActors_ActorId");

            migrationBuilder.CreateTable(
                name: "AppActors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppActors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSubActors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorId = table.Column<int>(type: "INT", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Enabled = table.Column<bool>(type: "BIT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubActors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubActors_AppActors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "AppActors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSubActors_ActorId",
                table: "AppSubActors",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppActors_ActorId",
                table: "AppSocialConflictActors",
                column: "ActorId",
                principalTable: "AppActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppSocialConflictActors_AppSubActors_SubActorId",
                table: "AppSocialConflictActors",
                column: "SubActorId",
                principalTable: "AppSubActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
