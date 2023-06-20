using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class BattleEntityTypeConfiguration : IEntityTypeConfiguration<Battle>
{
    public void Configure(EntityTypeBuilder<Battle> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasOne(x => x.Player1)
            .WithMany()
            .HasForeignKey(x => x.Player1Id)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Player2)
            .WithMany()
            .HasForeignKey(x => x.Player2Id)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Player1Deck)
            .WithMany()
            .HasForeignKey(x => x.Player1DeckId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Player2Deck)
            .WithMany()
            .HasForeignKey(x => x.Player2DeckId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.StartTime)
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .IsRequired(false);

        builder.Property(x => x.Winner)
            .IsRequired(false);
        
        builder.Property(x => x.Loser)
            .IsRequired(false);
    }
}