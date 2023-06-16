using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Shared;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Size)
            .IsRequired();

        builder.Property(x => x.MimeType)
            .IsRequired()
            .HasMaxLength(32);

        builder.HasDiscriminator<int>("ImageType")
            .HasValue<Image>(0)
            .HasValue<Skin>(1);
    }
}