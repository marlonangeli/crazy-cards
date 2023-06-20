using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CrazyCards.Persistence.Extensions;

internal static class ModelBuilderExtensions
{
    private static readonly ValueConverter<DateTime, DateTime> UtcValueConverter =
        new(outside => outside,
            inside => DateTime.SpecifyKind(inside, DateTimeKind.Utc));

    internal static void ApplyUtcDateTimeConverter(this ModelBuilder modelBuilder) =>
        modelBuilder.Model.GetEntityTypes()
            .ForEach(mutableEntityType => mutableEntityType
                .GetProperties()
                .Where(p => p.ClrType == typeof(DateTime) && p.Name.EndsWith("Utc", StringComparison.Ordinal))
                .ForEach(mutableProperty => mutableProperty.SetValueConverter(UtcValueConverter)));
    
    private static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        foreach (T obj in source)
        {
            action(obj);
        }
    }
}