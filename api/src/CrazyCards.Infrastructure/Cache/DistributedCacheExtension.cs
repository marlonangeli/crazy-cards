using CrazyCards.Domain.Extension;
using CrazyCards.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CrazyCards.Infrastructure.Cache;


public static class DistributedCacheExtension
{
    /// <summary>
    /// Configure Redis distributed cache
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddRedisDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = configuration.GetSection(RedisSettings.SectionName).Get<RedisSettings>()!;

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{redisOptions.Host}:{redisOptions.Port},password={redisOptions.Password}";
            options.InstanceName = redisOptions.InstanceName;
        });
    }
    
    public static async Task<T?> InsertAsync<T>(
        this IDistributedCache cache,
        string key,
        T value,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
    {
        await cache.SetStringAsync(
            key,
            JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = JsonConverterSetup.GetConverters()
            }),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            },
            cancellationToken);

        return value;
    }

    public static async Task<T?> GetAsync<T>(
        this IDistributedCache cache,
        string key,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetStringAsync(key, cancellationToken);

        return cachedValue is not null
            ? JsonConvert.DeserializeObject<T>(cachedValue, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = JsonConverterSetup.GetConverters()
            })
            : default;
    }
    
    /// <summary>
    /// Remove value from cache
    /// </summary>
    /// <param name="cache"><see cref="IDistributedCache"/> instance</param>
    /// <param name="key">Key to remove</param>
    /// <param name="cancellationToken"></param>
    public static async Task RemoveAsync(
        this IDistributedCache cache,
        string key,
        CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(key, cancellationToken);
    }

    /// <summary>
    /// Try to get the value from the cache, if it doesn't exist, call the function and cache the result.
    /// </summary>
    /// <param name="cache"><see cref="IDistributedCache"/> instance</param>
    /// <param name="key">Key to get the value</param>
    /// <param name="function">Function that's called</param>
    /// <param name="absoluteExpirationRelativeToNow">Expiration time</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">Type of result</typeparam>
    /// <returns>
    /// Result from the cache or the function
    /// </returns>
    public static async Task<T?> GetOrCallFunctionAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<Task<T>> function,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetAsync<T>(key, cancellationToken);
        
        if (cachedValue is not null)
        {
            return cachedValue;
        }
        
        var value = await function();
        
        await cache.InsertAsync(
            key,
            value,
            absoluteExpirationRelativeToNow,
            cancellationToken);
        
        return value;
    }

    /// <summary>
    /// Try to get the value from the cache, if it doesn't exist, call the function and cache the result.
    /// </summary>
    /// <param name="cache"><see cref="IDistributedCache"/> instance</param>
    /// <param name="key">Key to get the value</param>
    /// <param name="function">Function that's called</param>
    /// <param name="args">Arguments for function</param>
    /// <param name="absoluteExpirationRelativeToNow">Expiration time</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T">Type of result</typeparam>
    /// <typeparam name="TIn"></typeparam>
    /// <returns>
    /// Result from the cache or the function
    /// </returns>
    public static async Task<T?> GetOrCallFunctionAsync<TIn, T>(
        this IDistributedCache cache,
        string key,
        Func<TIn, Task<T>> function,
        TIn args,
        TimeSpan? absoluteExpirationRelativeToNow = null,
        CancellationToken cancellationToken = default)
    {
        var cachedValue = await cache.GetAsync<T>(key, cancellationToken);
        
        if (cachedValue is not null)
        {
            return cachedValue;
        }
        
        var value = await function.Invoke(args);
        
        await cache.InsertAsync(
            key,
            value,
            absoluteExpirationRelativeToNow,
            cancellationToken);
        
        return value;
    }
}