using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KingsBackMath.Migrations
{
    public partial class GameDefinitionAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Child_AspNetUsers_GameUserId1",
                table: "Child");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Child",
                table: "Child");

            migrationBuilder.RenameTable(
                name: "Child",
                newName: "Children");

            migrationBuilder.RenameIndex(
                name: "IX_Child_GameUserId1",
                table: "Children",
                newName: "IX_Children_GameUserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Children",
                table: "Children",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxNumber = table.Column<int>(type: "int", nullable: false),
                    MinNumber = table.Column<int>(type: "int", nullable: false),
                    Rounds = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDefinitions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Children_AspNetUsers_GameUserId1",
                table: "Children",
                column: "GameUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_AspNetUsers_GameUserId1",
                table: "Children");

            migrationBuilder.DropTable(
                name: "GameDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Children",
                table: "Children");

            migrationBuilder.RenameTable(
                name: "Children",
                newName: "Child");

            migrationBuilder.RenameIndex(
                name: "IX_Children_GameUserId1",
                table: "Child",
                newName: "IX_Child_GameUserId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Child",
                table: "Child",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Child_AspNetUsers_GameUserId1",
                table: "Child",
                column: "GameUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
