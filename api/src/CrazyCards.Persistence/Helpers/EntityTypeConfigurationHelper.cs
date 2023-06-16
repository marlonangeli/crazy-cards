using CrazyCards.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyCards.Persistence.Helpers;

public static class EntityTypeConfigurationHelper
{
    public static void ConfigureBaseEntity<T> (this EntityTypeBuilder<T> builder) where T : Entity
    {
        builder.ToTable(typeof(T).Name)
            .HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
    }
}