using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ideas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ideas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LeftId = table.Column<Guid>(type: "uuid", nullable: true),
                    RightId = table.Column<Guid>(type: "uuid", nullable: true),
                    WinnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoserId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Ideas_LeftId",
                        column: x => x.LeftId,
                        principalTable: "Ideas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Ideas_LoserId",
                        column: x => x.LoserId,
                        principalTable: "Ideas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Ideas_RightId",
                        column: x => x.RightId,
                        principalTable: "Ideas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Ideas_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Ideas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdeaId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.UniqueConstraint("AK_Tags_IdeaId_Name", x => new { x.IdeaId, x.Name });
                    table.ForeignKey(
                        name: "FK_Tags_Ideas_IdeaId",
                        column: x => x.IdeaId,
                        principalTable: "Ideas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_LeftId",
                table: "Games",
                column: "LeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_LoserId",
                table: "Games",
                column: "LoserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RightId",
                table: "Games",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Ideas");
        }
    }
}
