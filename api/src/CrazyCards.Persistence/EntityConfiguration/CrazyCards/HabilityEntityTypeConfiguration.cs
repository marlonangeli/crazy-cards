using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Enum;
using CrazyCards.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.EntityConfiguration.CrazyCards;

public class HabilityEntityTypeConfiguration : IEntityTypeConfiguration<Hability>
{
    public void Configure(EntityTypeBuilder<Hability> builder)
    {
        builder.ConfigureBaseEntity();

        builder.OwnsOne(x => x.Action, action =>
        {
            action.HasOne(a => a.InvokeCard)
                .WithMany()
                .HasForeignKey(a => a.InvokeCardId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            action.Property(a => a.Description)
                .HasMaxLength(256)
                .IsRequired(false);

            action.Property(a => a.Damage)
                .IsRequired(false);
            
            action.Property(a => a.Heal)
                .IsRequired(false);
            
            action.Property(a => a.Shield)
                .IsRequired(false);
            
            action.Property(a => a.DamageToAll)
                .HasDefaultValue(false);
            
            action.Property(a => a.HealToAll)
                .HasDefaultValue(false);
            
            action.Property(a => a.ShieldToAll)
                .HasDefaultValue(false);
            
            action.Property(a => a.InvokeCardType)
                .HasConversion(cardType => cardType!.Value, x => CardType.FromValue(x)!)
                .IsRequired(false);
        });
        
        builder.Property(x => x.Type)
            .HasConversion(habilityType => habilityType.Value, x => HabilityType.FromValue(x)!)
            .IsRequired();

        builder.HasDiscriminator(x => x.Type)
            .HasValue<BattlecryHability>(HabilityType.Battlecry)
            .HasValue<TauntHability>(HabilityType.Taunt)
            .HasValue<LastBreathHability>(HabilityType.LastBreath);

        builder.HasOne(h => h.Card)
            .WithMany(c => c.Habilities)
            .HasForeignKey(h => h.CardId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}