using BookChoice.Movie.Application.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace BookChoice.Movie.Infrastructure.MovieServices;

internal static class Startup
{
    internal static IServiceCollection AddMovieServices(this IServiceCollection services)
    {
        services.AddScoped<IOmdbService, OmdbService>();
        services.AddScoped<IYtService, YtService>();
        return services;
    }
        
}