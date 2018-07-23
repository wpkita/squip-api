using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.WebApi.Migrations
{
    public partial class TenantQueryFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Tag",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "SquipTag",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "SquipTag",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "SquipTag",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Squip",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Squip");
        }
    }
}
