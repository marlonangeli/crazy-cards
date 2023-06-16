using CrazyCards.Domain.Entities.Card;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class HeroEntityTypeConfiguration : IEntityTypeConfiguration<Hero>
{
    public void Configure(EntityTypeBuilder<Hero> builder)
    {
        builder.ConfigureBaseEntity();
        
        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => x.Name);

        builder.Property(x => x.Description)
            .HasMaxLength(512)
            .IsRequired();
        
        builder.HasOne(h => h.Skin)
            .WithMany()
            .HasForeignKey(h => h.SkinId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(h => h.Class)
            .WithMany()
            .HasForeignKey(h => h.ClassId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(h => h.Image)
            .WithMany()
            .HasForeignKey(h => h.ImageId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(h => h.Weapon)
            .WithMany()
            .HasForeignKey(h => h.WeaponId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}