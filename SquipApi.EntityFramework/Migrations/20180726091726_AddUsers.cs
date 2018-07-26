using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SquipApi.EntityFramework.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "Squip",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<string>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ThirdPartyId = table.Column<string>(nullable: false),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_ThirdPartyId", x => x.ThirdPartyId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squip_User_CreatedByUserId",
                table: "Squip");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Squip_CreatedByUserId",
                table: "Squip");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Squip");
        }
    }
}
