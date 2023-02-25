using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class AddTargets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Targets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TargetEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstantOccurredAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    Magnitude = table.Column<int>(type: "integer", nullable: false),
                    DidEngage = table.Column<bool>(type: "boolean", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TargetEntries_Targets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TargetEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TargetEntries_TargetId",
                table: "TargetEntries",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_TargetEntries_UserId",
                table: "TargetEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Targets_UserId",
                table: "Targets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetEntries");

            migrationBuilder.DropTable(
                name: "Targets");
        }
    }
}
