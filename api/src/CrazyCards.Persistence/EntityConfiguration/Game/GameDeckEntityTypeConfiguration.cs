using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.Game;

public class GameDeckEntityTypeConfiguration : IEntityTypeConfiguration<GameDeck>
{
    public void Configure(EntityTypeBuilder<GameDeck> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.HasOne(x => x.Battle)
            .WithMany()
            .HasForeignKey(x => x.BattleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Cards)
            .WithOne(x => x.GameDeck)
            .HasForeignKey(x => x.GameDeckId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}