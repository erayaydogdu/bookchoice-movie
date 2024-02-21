namespace BookChoice.Movie.Api.Extensions;

internal static class ConfigurationExtention
{
    internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        return builder;
    }
}