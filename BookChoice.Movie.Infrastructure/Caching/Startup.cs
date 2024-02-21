using BookChoice.Movie.Application.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookChoice.Movie.Infrastructure.Caching;

internal static class Startup
{
    internal static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration config)
    {
        // services.AddStackExchangeRedisCache(options =>
        // {
        //     options.Configuration = "localhost:6379";
        //     options.ConfigurationOptions = new StackExchange.Redis.ConfigurationOptions()
        //     {
        //         AbortOnConnectFail = true,
        //         EndPoints = { "localhost:6379" }
        //     };
        // });
        services.AddDistributedMemoryCache();
        services.AddTransient<ICacheService, DistributedCacheService>();
        return services;
    }
}