using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Enum;
using CrazyCards.Persistence.Constants;
using CrazyCards.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class CardEntityTypeConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ConfigureBaseEntity();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100)
            .UseCollation(SqlCollation.PortugueseBrazilianCaseInsensitive);
        
        builder.HasIndex(x => x.Name);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(x => x.Type)
            .HasConversion(cardType => cardType.Value, x => CardType.FromValue(x)!)
            .IsRequired();

        builder.Property(x => x.Rarity)
            .HasConversion(rarity => rarity.Value, x => Rarity.FromValue(x)!)
            .IsRequired();

        builder.Property(x => x.ManaCost)
            .IsRequired();

        builder.HasDiscriminator(x => x.Type)
            .HasValue<MinionCard>(CardType.Minion)
            .HasValue<SpellCard>(CardType.Spell)
            .HasValue<WeaponCard>(CardType.Weapon)
            .HasValue<TotenCard>(CardType.Toten);

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

        builder.HasOne(x => x.Class)
            .WithMany()
            .HasForeignKey(x => x.ClassId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Habilities)
            .WithOne(x => x.Card)
            .HasForeignKey(x => x.CardId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}