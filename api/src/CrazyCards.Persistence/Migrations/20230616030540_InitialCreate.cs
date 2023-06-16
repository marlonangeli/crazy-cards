using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrazyCards.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ImageType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Username = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PlayerDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Class_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Class_Image_SkinId",
                        column: x => x.SkinId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerDeck_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManaCost = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: true),
                    Health = table.Column<int>(type: "int", nullable: true),
                    Damage = table.Column<int>(type: "int", nullable: true),
                    Heal = table.Column<int>(type: "int", nullable: true),
                    TotenCard_Heal = table.Column<int>(type: "int", nullable: true),
                    Shield = table.Column<int>(type: "int", nullable: true),
                    WeaponCard_Damage = table.Column<int>(type: "int", nullable: true),
                    Durability = table.Column<int>(type: "int", nullable: true),
                    WeaponCard_Shield = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Card_Image_SkinId",
                        column: x => x.SkinId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action_Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Action_InvokeCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Action_InvokeCardType = table.Column<int>(type: "int", nullable: true),
                    Action_Damage = table.Column<int>(type: "int", nullable: true),
                    Action_Heal = table.Column<int>(type: "int", nullable: true),
                    Action_Shield = table.Column<int>(type: "int", nullable: true),
                    Action_DamageToAll = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Action_HealToAll = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Action_ShieldToAll = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hability_Card_Action_InvokeCardId",
                        column: x => x.Action_InvokeCardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Hability_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hero",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeaponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hero_Card_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hero_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hero_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hero_Image_SkinId",
                        column: x => x.SkinId,
                        principalTable: "Image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BattleDeck",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PlayerDeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleDeck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleDeck_Hero_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Hero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleDeck_PlayerDeck_PlayerDeckId",
                        column: x => x.PlayerDeckId,
                        principalTable: "PlayerDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleDeckCard",
                columns: table => new
                {
                    DeckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleDeckCard", x => new { x.DeckId, x.CardId });
                    table.ForeignKey(
                        name: "FK_BattleDeckCard_BattleDeck_DeckId",
                        column: x => x.DeckId,
                        principalTable: "BattleDeck",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleDeckCard_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleDeck_HeroId",
                table: "BattleDeck",
                column: "HeroId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleDeck_PlayerDeckId",
                table: "BattleDeck",
                column: "PlayerDeckId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleDeckCard_CardId",
                table: "BattleDeckCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ClassId",
                table: "Card",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ImageId",
                table: "Card",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Name",
                table: "Card",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Card_SkinId",
                table: "Card",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_ImageId",
                table: "Class",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_SkinId",
                table: "Class",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_Hability_Action_InvokeCardId",
                table: "Hability",
                column: "Action_InvokeCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Hability_CardId",
                table: "Hability",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_ClassId",
                table: "Hero",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_ImageId",
                table: "Hero",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_SkinId",
                table: "Hero",
                column: "SkinId");

            migrationBuilder.CreateIndex(
                name: "IX_Hero_WeaponId",
                table: "Hero",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerDeck_PlayerId",
                table: "PlayerDeck",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleDeckCard");

            migrationBuilder.DropTable(
                name: "Hability");

            migrationBuilder.DropTable(
                name: "BattleDeck");

            migrationBuilder.DropTable(
                name: "Hero");

            migrationBuilder.DropTable(
                name: "PlayerDeck");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
