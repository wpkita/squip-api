using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.WebApi.Migrations
{
    public partial class JoinWithTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SquipTag",
                columns: table => new
                {
                    SquipId = table.Column<long>(nullable: false),
                    TagName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquipTag", x => new { x.TagName, x.SquipId });
                    table.ForeignKey(
                        name: "FK_SquipTag_Squips_SquipId",
                        column: x => x.SquipId,
                        principalTable: "Squips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquipTag_Tags_TagName",
                        column: x => x.TagName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SquipTag_SquipId",
                table: "SquipTag",
                column: "SquipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SquipTag");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
