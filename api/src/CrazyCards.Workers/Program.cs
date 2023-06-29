using CrazyCards.Application.Interfaces;
using CrazyCards.Infrastructure.Settings;
using CrazyCards.Persistence.Context;
using CrazyCards.Workers;
using Microsoft.EntityFrameworkCore;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host, services) =>
    {
        var crazyCardsDbConnectionString = host.Configuration.GetConnectionString(SqlServerSettings.CrazyCardsDb);
        services.AddDbContext<CrazyCardsDbContext>(options =>
        {
            options.UseSqlServer(crazyCardsDbConnectionString,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        services.AddScoped<IDbContext, CrazyCardsDbContext>();

        services.AddHostedService<Worker>();
    })
    .UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration))
    .Build();

await host.RunAsync();