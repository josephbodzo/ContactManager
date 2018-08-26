using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Core.Repositories.DatabaseContext.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneBookEntry",
                columns: table => new
                {
                    PhoneBookId = table.Column<int>(nullable: false),
                    PhoneEntryId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBookEntry", x => new { x.PhoneBookId, x.PhoneEntryId });
                    table.ForeignKey(
                        name: "FK_PhoneBookEntry_PhoneBooks_PhoneBookId",
                        column: x => x.PhoneBookId,
                        principalTable: "PhoneBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneBookEntry_PhoneEntries_PhoneEntryId",
                        column: x => x.PhoneEntryId,
                        principalTable: "PhoneEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhoneBookEntry_PhoneEntryId",
                table: "PhoneBookEntry",
                column: "PhoneEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneBookEntry");

            migrationBuilder.DropTable(
                name: "PhoneBooks");

            migrationBuilder.DropTable(
                name: "PhoneEntries");
        }
    }
}
