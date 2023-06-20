using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class BattleDeckEntityTypeConfiguration : IEntityTypeConfiguration<BattleDeck>
{
    public void Configure(EntityTypeBuilder<BattleDeck> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasMany(x => x.Cards)
            .WithMany()
            .UsingEntity<BattleDeckCard>(
                x =>
                    x.HasOne(x => x.Card)
                        .WithMany()
                        .HasForeignKey(x => x.CardId),
                x =>
                    x.HasOne(x => x.Deck)
                        .WithMany()
                        .HasForeignKey(x => x.DeckId),
                x =>
                    x.HasKey(x => new { x.DeckId, x.CardId }));
        
        builder.HasOne(x => x.PlayerDeck)
            .WithMany(x => x.BattleDecks)
            .HasForeignKey(x => x.PlayerDeckId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired();
        
        builder.HasOne(x => x.Hero)
            .WithMany()
            .HasForeignKey(x => x.HeroId)
            .IsRequired();
    }
}