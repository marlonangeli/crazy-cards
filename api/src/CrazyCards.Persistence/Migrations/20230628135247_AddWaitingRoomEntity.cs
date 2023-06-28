using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWaitingRoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WaitingRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsWaiting = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BattleDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitingRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaitingRoom_BattleDeck_BattleDeckId",
                        column: x => x.BattleDeckId,
                        principalTable: "BattleDeck",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WaitingRoom_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaitingRoom_BattleDeckId",
                table: "WaitingRoom",
                column: "BattleDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_WaitingRoom_PlayerId",
                table: "WaitingRoom",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WaitingRoom");
        }
    }
}
