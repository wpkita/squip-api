using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class UseUsersEverywhere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squip_User_CreatedByUserId",
                table: "Squip");

            migrationBuilder.DropIndex(
                name: "IX_Squip_CreatedByUserId",
                table: "Squip");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Tag",
                nullable: false,
                defaultValue: 2L);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByUserId",
                table: "Tag",
                nullable: false,
                defaultValue: 2L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "SquipTag",
                nullable: false,
                defaultValue: 2L);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByUserId",
                table: "SquipTag",
                nullable: false,
                defaultValue: 2L);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedByUserId",
                table: "Squip",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ModifiedByUserId",
                table: "Squip",
                nullable: false,
                defaultValue: 2L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "Squip");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                name: "CreatedByUserId",
                table: "Squip",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Squip_CreatedByUserId",
                table: "Squip",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Squip_User_CreatedByUserId",
                table: "Squip",
                column: "CreatedByUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
