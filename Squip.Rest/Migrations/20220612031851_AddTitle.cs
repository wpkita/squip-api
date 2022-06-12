#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Squip.Rest.Migrations
{
    public partial class AddTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Content", table: "Ideas", newName: "Title");
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Ideas",
                type: "nvarchar(450)",
                defaultValue: string.Empty
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Title", table: "Ideas", newName: "Content");
            migrationBuilder.DropColumn(name: "Title", table: "Ideas");
        }
    }
}
