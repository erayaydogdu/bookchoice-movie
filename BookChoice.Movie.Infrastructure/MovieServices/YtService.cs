using System.Text.Json;
using BookChoice.Movie.Application.Movies;
using Microsoft.Extensions.Configuration;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace BookChoice.Movie.Infrastructure.MovieServices;

public class YtService : IYtService
{
    private readonly IConfiguration _configuration;
    private string _apiKey;

    public YtService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _apiKey = _configuration.GetValue<string>("YoutubeApi:ApiKey");
    }

    public async Task<List<MovieVideosDto>> GetMovieVideosAsync(string movieName, int count = 10)
    {
        if (string.IsNullOrEmpty(movieName))
        {
            throw new ArgumentException("A movie name must be provided", nameof(movieName));
        }

        List<MovieVideosDto> response = new List<MovieVideosDto>();
        
        var youtubeService = new YouTubeService(new BaseClientService.Initializer() {
            ApiKey = _apiKey,
            ApplicationName = nameof(YtService)
        });
        
        var searchListRequest = youtubeService.Search.List("snippet");
        searchListRequest.Q = movieName; 
        searchListRequest.MaxResults = count;
        var searchListResponse = await searchListRequest.ExecuteAsync();

        foreach (var searchResult in searchListResponse.Items)
        {
            response.Add(new MovieVideosDto
            {
                ThumbUrl = searchResult.Snippet.Thumbnails.Default__.Url,
                VideoUrl = searchResult.Snippet.Thumbnails.Default__.Url
            });
        }
        
        return response;
    }
}