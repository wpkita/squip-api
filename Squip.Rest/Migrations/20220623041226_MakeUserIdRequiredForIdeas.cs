using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class MakeUserIdRequiredForIdeas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ideas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Ideas",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
