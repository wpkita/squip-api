using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squip.Rest.Migrations
{
    public partial class AddGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        LeftId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        RightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        LoserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                        InstantCreatedAt = table.Column<DateTime>(
                            type: "datetime2",
                            nullable: false
                        ),
                        InstantUpdatedAt = table.Column<DateTime>(
                            type: "datetime2",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Ideas_LeftId",
                        column: x => x.LeftId,
                        principalTable: "Ideas",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Games_Ideas_LoserId",
                        column: x => x.LoserId,
                        principalTable: "Ideas",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Games_Ideas_RightId",
                        column: x => x.RightId,
                        principalTable: "Ideas",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Games_Ideas_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Ideas",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(name: "IX_Games_LeftId", table: "Games", column: "LeftId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_LoserId",
                table: "Games",
                column: "LoserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Games_RightId",
                table: "Games",
                column: "RightId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Games_WinnerId",
                table: "Games",
                column: "WinnerId"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Games");
        }
    }
}
