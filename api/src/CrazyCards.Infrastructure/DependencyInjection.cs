using CrazyCards.Application.Interfaces.Services;
using CrazyCards.Infrastructure.Cache;
using CrazyCards.Infrastructure.Settings;
using CrazyCards.Infrastructure.Storage;
using CrazyCards.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Core.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection.Configuration;

namespace CrazyCards.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var crazyCardsDbConnectionString = configuration.GetConnectionString(SqlServerSettings.CrazyCardsDb);
        var gameDbConnectionString = configuration.GetConnectionString(SqlServerSettings.GameDb);

        services.AddDbContext<CrazyCardsDbContext>(options =>
        {
            options.UseSqlServer(crazyCardsDbConnectionString);
            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });
        services.AddDbContext<GameDbContext>(options =>
        {
            options.UseSqlServer(gameDbConnectionString);
            if (environment.IsDevelopment())
            {
                options.EnableSensitiveDataLogging();
            }
        });

        // var rabbitMqOptions = configuration.GetSection(RabbitMQSettings.SectionName).Get<RabbitMQSettings>()!;
        // var rabbitMqServiceOptions = new RabbitMqServiceOptions
        // {
        //     HostName = rabbitMqOptions.Host,
        //     Port = rabbitMqOptions.Port,
        //     UserName = rabbitMqOptions.Username,
        //     Password = rabbitMqOptions.Password
        // };
        // services.AddRabbitMqProducer(rabbitMqServiceOptions);
        services.AddSingleton<IBlobStorageService>(new BlobStorageService(configuration));
        services.AddRedisDistributedCache(configuration);

        return services;
    }
}