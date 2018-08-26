using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Core.Repositories.DatabaseContext.Migrations
{
    public partial class AddDateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PhoneEntries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "PhoneEntries",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PhoneBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "PhoneBooks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "PhoneBookEntry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "PhoneBookEntry",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PhoneEntries");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "PhoneEntries");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PhoneBooks");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "PhoneBooks");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "PhoneBookEntry");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "PhoneBookEntry");
        }
    }
}
