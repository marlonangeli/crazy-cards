using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBattleColumnInWaitingRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BattleId",
                table: "WaitingRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitingRoom_BattleId",
                table: "WaitingRoom",
                column: "BattleId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingRoom_Battle_BattleId",
                table: "WaitingRoom",
                column: "BattleId",
                principalTable: "Battle",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaitingRoom_Battle_BattleId",
                table: "WaitingRoom");

            migrationBuilder.DropIndex(
                name: "IX_WaitingRoom_BattleId",
                table: "WaitingRoom");

            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "WaitingRoom");
        }
    }
}
