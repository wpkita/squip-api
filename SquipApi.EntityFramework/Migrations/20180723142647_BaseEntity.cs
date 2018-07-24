using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class BaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Tag",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Tag",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Squip",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Squip",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Squip");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Squip");
        }
    }
}
