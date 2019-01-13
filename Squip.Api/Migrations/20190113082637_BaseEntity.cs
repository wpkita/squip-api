using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Squip.Api.Migrations
{
    public partial class BaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Squips",
                table: "Squips");

            migrationBuilder.RenameTable(
                name: "Squips",
                newName: "squips");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "squips",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "squips",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "squips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "squips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_squips",
                table: "squips",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_squips",
                table: "squips");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "squips");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "squips");

            migrationBuilder.RenameTable(
                name: "squips",
                newName: "Squips");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Squips",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Squips",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Squips",
                table: "Squips",
                column: "Id");
        }
    }
}
