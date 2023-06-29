using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.Game;

public class WaitingRoomEntityTypeConfiguration : IEntityTypeConfiguration<WaitingRoom>
{
    public void Configure(EntityTypeBuilder<WaitingRoom> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.BattleDeck)
            .WithMany()
            .HasForeignKey(x => x.BattleDeckId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Battle)
            .WithMany()
            .HasForeignKey(x => x.BattleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
    }
}