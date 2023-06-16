using CrazyCards.Domain.Entities.Card;
using CrazyCards.Persistence.Constants;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class ClassEntityTypeConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(32)
            .UseCollation(SqlCollation.PortugueseBrazilianCaseInsensitive);

        builder.HasIndex(x => x.Name);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(256);
        
        builder.HasOne(x => x.Image)
            .WithMany()
            .HasForeignKey(x => x.ImageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Skin)
            .WithMany()
            .HasForeignKey(x => x.SkinId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Cards)
            .WithOne(x => x.Class)
            .HasForeignKey(x => x.ClassId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}