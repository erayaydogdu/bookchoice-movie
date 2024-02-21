using System.Text.Json;
using BookChoice.Movie.Application.Movies;
using Microsoft.Extensions.Configuration;

namespace BookChoice.Movie.Infrastructure.MovieServices;

public class OmdbService : IOmdbService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private string _apiKey;
    private string _baseUrl;

    public OmdbService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        _baseUrl = _configuration.GetValue<string>("OmdbApi:BaseUrl");
        _apiKey = _configuration.GetValue<string>("OmdbApi:ApiKey");
    }

    public async Task<MovieDto> GetMovieDetailsAsync(string movieName)
    {
        if (string.IsNullOrEmpty(movieName))
        {
            throw new ArgumentException(nameof(movieName));
        }

        var client = _httpClientFactory.CreateClient(nameof(OmdbService));
        client.BaseAddress = new Uri(_baseUrl);
        HttpResponseMessage response = await client.GetAsync($"?apikey={_apiKey}&t={movieName}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Could not retrieve movie details");
        }

        var json = await response.Content.ReadAsStringAsync();
        var movieDetails = JsonSerializer.Deserialize<MovieDto>(json);
        return movieDetails;
    }
}