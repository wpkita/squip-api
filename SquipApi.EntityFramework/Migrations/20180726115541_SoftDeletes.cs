using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class SoftDeletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Tag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "SquipTag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Squip",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Squip");
        }
    }
}
