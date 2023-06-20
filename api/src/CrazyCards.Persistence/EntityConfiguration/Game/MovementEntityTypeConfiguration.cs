using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.Game;

public class MovementEntityTypeConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.HasOne(x => x.Round)
            .WithMany(x => x.Movements)
            .HasForeignKey(x => x.RoundId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.CardInitiator)
            .WithMany()
            .HasForeignKey(x => x.CardInitiatorId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.CardTarget)
            .WithMany()
            .HasForeignKey(x => x.CardTargetId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}