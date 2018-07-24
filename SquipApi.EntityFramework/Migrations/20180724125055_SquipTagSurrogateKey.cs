using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class SquipTagSurrogateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SquipTag",
                table: "SquipTag");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "SquipTag",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SquipTag",
                table: "SquipTag",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SquipTag_TagId_SquipId",
                table: "SquipTag",
                columns: new[] { "TagId", "SquipId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SquipTag",
                table: "SquipTag");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SquipTag_TagId_SquipId",
                table: "SquipTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SquipTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SquipTag",
                table: "SquipTag",
                columns: new[] { "TagId", "SquipId" });
        }
    }
}
