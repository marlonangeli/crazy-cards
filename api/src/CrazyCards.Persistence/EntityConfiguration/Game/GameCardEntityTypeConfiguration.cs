using CrazyCards.Domain.Entities.Game;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.Game;

public class GameCardEntityTypeConfiguration : IEntityTypeConfiguration<GameCard>
{
    public void Configure(EntityTypeBuilder<GameCard> builder)
    {
        builder.ConfigureBaseEntity();

        builder.HasOne(x => x.OriginalCard)
            .WithMany()
            .HasForeignKey(x => x.OriginalCardId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.OwnsOne(x => x.Current);

        builder.OwnsOne(x => x.Is);
        
        builder.OwnsOne(x => x.At);
    }
}