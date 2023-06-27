using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class AddHibits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hibits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InstantOccurredAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    HabitId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    InstantCreatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    InstantUpdatedAt = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hibits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hibits_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hibits_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hibits_HabitId",
                table: "Hibits",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_Hibits_UserId",
                table: "Hibits",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hibits");
        }
    }
}
