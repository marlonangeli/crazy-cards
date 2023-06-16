using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.Game;

public class RoundEntityTypeConfiguration : IEntityTypeConfiguration<Round>
{
    public void Configure(EntityTypeBuilder<Round> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.HasOne(x => x.Battle)
            .WithMany(x => x.Rounds)
            .HasForeignKey(x => x.BattleId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x => x.Movements)
            .WithOne(x => x.Round)
            .HasForeignKey(x => x.RoundId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Number)
            .IsRequired();
    }
}