using CrazyCards.Domain.Entities.Deck;
using CrazyCards.Domain.Entities.Player;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.Property(x => x.Username)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(p => p.PlayerDeck)
            .WithOne(playerDeck => playerDeck.Player)
            .HasForeignKey<PlayerDeck>(playerDeck => playerDeck.PlayerId)
            .IsRequired();
    }
}