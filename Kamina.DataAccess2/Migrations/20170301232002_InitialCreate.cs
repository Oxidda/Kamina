using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamina.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuildName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CalenderItemId = table.Column<Guid>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalenderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    GuildId = table.Column<ulong>(nullable: true),
                    Recurrance = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalenderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalenderItems_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalenderItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalenderItems_GuildId",
                table: "CalenderItems",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_CalenderItems_UserId",
                table: "CalenderItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CalenderItemId",
                table: "Users",
                column: "CalenderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GuildId",
                table: "Users",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CalenderItems_CalenderItemId",
                table: "Users",
                column: "CalenderItemId",
                principalTable: "CalenderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalenderItems_Guilds_GuildId",
                table: "CalenderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Guilds_GuildId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_CalenderItems_Users_UserId",
                table: "CalenderItems");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CalenderItems");
        }
    }
}
