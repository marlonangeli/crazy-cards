using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGameEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1DeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2DeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Winner = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Loser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battle_BattleDeck_Player1DeckId",
                        column: x => x.Player1DeckId,
                        principalTable: "BattleDeck",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battle_BattleDeck_Player2DeckId",
                        column: x => x.Player2DeckId,
                        principalTable: "BattleDeck",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battle_Player_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Battle_Player_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BattleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameDeck_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Round",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    BattleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Round", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Round_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Current_ManaCost = table.Column<int>(type: "int", nullable: false),
                    Current_Health = table.Column<int>(type: "int", nullable: false),
                    Current_Attack = table.Column<int>(type: "int", nullable: false),
                    Current_Heal = table.Column<int>(type: "int", nullable: false),
                    Current_Shield = table.Column<int>(type: "int", nullable: false),
                    Current_Durability = table.Column<int>(type: "int", nullable: false),
                    Current_SpellDamage = table.Column<int>(type: "int", nullable: false),
                    Is_Silenced = table.Column<bool>(type: "bit", nullable: false),
                    Is_Poisoned = table.Column<bool>(type: "bit", nullable: false),
                    Is_Stealth = table.Column<bool>(type: "bit", nullable: false),
                    Is_Frozen = table.Column<bool>(type: "bit", nullable: false),
                    Is_Sleeping = table.Column<bool>(type: "bit", nullable: false),
                    Is_Dead = table.Column<bool>(type: "bit", nullable: false),
                    Is_Shielded = table.Column<bool>(type: "bit", nullable: false),
                    Is_Taunt = table.Column<bool>(type: "bit", nullable: false),
                    At_Table = table.Column<bool>(type: "bit", nullable: false),
                    At_Hand = table.Column<bool>(type: "bit", nullable: false),
                    At_Deck = table.Column<bool>(type: "bit", nullable: false),
                    At_Graveyard = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCard_Card_OriginalCardId",
                        column: x => x.OriginalCardId,
                        principalTable: "Card",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameCard_GameDeck_GameDeckId",
                        column: x => x.GameDeckId,
                        principalTable: "GameDeck",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardInitiatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardTargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movement_GameCard_CardInitiatorId",
                        column: x => x.CardInitiatorId,
                        principalTable: "GameCard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_GameCard_CardTargetId",
                        column: x => x.CardTargetId,
                        principalTable: "GameCard",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movement_Round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Round",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Player1DeckId",
                table: "Battle",
                column: "Player1DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Player1Id",
                table: "Battle",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Player2DeckId",
                table: "Battle",
                column: "Player2DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Player2Id",
                table: "Battle",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameCard_GameDeckId",
                table: "GameCard",
                column: "GameDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCard_OriginalCardId",
                table: "GameCard",
                column: "OriginalCardId");

            migrationBuilder.CreateIndex(
                name: "IX_GameDeck_BattleId",
                table: "GameDeck",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_CardInitiatorId",
                table: "Movement",
                column: "CardInitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_CardTargetId",
                table: "Movement",
                column: "CardTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_RoundId",
                table: "Movement",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Round_BattleId",
                table: "Round",
                column: "BattleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movement");

            migrationBuilder.DropTable(
                name: "GameCard");

            migrationBuilder.DropTable(
                name: "Round");

            migrationBuilder.DropTable(
                name: "GameDeck");

            migrationBuilder.DropTable(
                name: "Battle");
        }
    }
}
