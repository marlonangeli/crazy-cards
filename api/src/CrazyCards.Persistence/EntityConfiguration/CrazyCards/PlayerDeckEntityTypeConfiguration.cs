using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class PlayerDeckEntityTypeConfiguration : IEntityTypeConfiguration<PlayerDeck>
{
    public void Configure(EntityTypeBuilder<PlayerDeck> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.HasMany(x => x.BattleDecks)
            .WithOne(x => x.PlayerDeck)
            .HasForeignKey(x => x.PlayerDeckId)
            .IsRequired();

        builder.HasOne(x => x.Player)
            .WithOne(player => player.PlayerDeck)
            .HasForeignKey<PlayerDeck>(playerDeck => playerDeck.PlayerId)
            .IsRequired();
    }
}