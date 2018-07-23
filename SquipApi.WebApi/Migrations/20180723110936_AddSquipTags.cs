using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.WebApi.Migrations
{
    public partial class AddSquipTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Squips",
                table: "Squips");

            migrationBuilder.RenameTable(
                name: "Squips",
                newName: "Squip");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Squip",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Squip",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Squip",
                table: "Squip",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.UniqueConstraint("AK_Tag_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SquipTag",
                columns: table => new
                {
                    SquipId = table.Column<long>(nullable: false),
                    TagId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquipTag", x => new { x.TagId, x.SquipId });
                    table.ForeignKey(
                        name: "FK_SquipTag_Squip_SquipId",
                        column: x => x.SquipId,
                        principalTable: "Squip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquipTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
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
                name: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Squip",
                table: "Squip");

            migrationBuilder.RenameTable(
                name: "Squip",
                newName: "Squips");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Squips",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Body",
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
