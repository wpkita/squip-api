using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class RemoveTenantId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Squip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "User",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                table: "Tag",
                nullable: false,
                defaultValue: "");

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
    }
}
