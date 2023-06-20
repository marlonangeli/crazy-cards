using CrazyCards.Persistence.Context;

namespace CrazyCards.Api.Extensions;

public static class ApplicationBuilderExtension
{
    internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
    {
        using IServiceScope serviceScope = builder.ApplicationServices.CreateScope();

        using CrazyCardsDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<CrazyCardsDbContext>();

        dbContext.Database.EnsureCreated();

        // TODO - Criar método para popular o banco de dados
        // dbContext.SeedDatabase();

        return builder;
    }
}