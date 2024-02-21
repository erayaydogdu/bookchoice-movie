using System.Reflection;
using BookChoice.Movie.Infrastructure.Caching;
using BookChoice.Movie.Infrastructure.MovieServices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookChoice.Movie.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(BookChoice.Movie.Application.Startup).GetTypeInfo().Assembly;
        return services
            .AddCaching(config)
            .AddHttpClient()
            //.AddExceptionMiddleware()
            //.AddBehaviours(applicationAssembly)
            .AddHealthCheck()
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddRouting()
            .AddMovieServices();
    }
    
    private static IServiceCollection AddHealthCheck(this IServiceCollection services) =>
        services.AddHealthChecks().Services;
    
    
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers();
        builder.MapHealthCheck();
        return builder;
    }

    private static IEndpointConventionBuilder MapHealthCheck(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapHealthChecks("/api/health");
}