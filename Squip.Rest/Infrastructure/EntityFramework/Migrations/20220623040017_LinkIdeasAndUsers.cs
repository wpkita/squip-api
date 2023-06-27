using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class LinkIdeasAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Ideas",
                type: "uuid",
                nullable: true
            );

            migrationBuilder.CreateIndex(name: "IX_Ideas_UserId", table: "Ideas", column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ideas_Users_UserId",
                table: "Ideas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Ideas_Users_UserId", table: "Ideas");

            migrationBuilder.DropIndex(name: "IX_Ideas_UserId", table: "Ideas");

            migrationBuilder.DropColumn(name: "UserId", table: "Ideas");
        }
    }
}
