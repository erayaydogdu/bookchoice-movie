using System.Text.Json;
using BookChoice.Movie.Application.Common;
using BookChoice.Movie.Application.Mapping;
using BookChoice.Movie.Domain.Movies;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace BookChoice.Movie.Application.Movies;

public class SearchMovieRequest : IRequest<MovieResponseDto>
{
    public string Title { get; set; }
}

public class SearchMovieRequestHandler : IRequestHandler<SearchMovieRequest, MovieResponseDto>
{
    private readonly IDistributedCache _cache;
    private readonly IOmdbService _omdbService;
    private readonly IYtService _ytService;

    
    public SearchMovieRequestHandler(IOmdbService omdbService, IDistributedCache cache, IYtService ytService)
    {
        _omdbService = omdbService;
        _cache = cache;
        _ytService = ytService;
    }
    
    public async Task<MovieResponseDto> Handle(SearchMovieRequest request, CancellationToken cancellationToken)
    {
        string cachedResponse = await _cache.GetStringAsync(request.Title);
        Domain.Movies.Movie movie = null;
        if (string.IsNullOrEmpty(cachedResponse))
        {
            var movieTitle = request.Title;
            // for parallel calls to get movie details from OMDB and movie videos from Youtube
            var movieDetailsTask = _omdbService.GetMovieDetailsAsync(movieTitle);
            var movieVideosTask = _ytService.GetMovieVideosAsync(movieTitle, 5);

            await Task.WhenAll(movieVideosTask,movieDetailsTask);
            var movieDto = movieDetailsTask.Result;
            var movieVideoDtos = movieVideosTask.Result;
            
            // data aggregation
            movie = new Domain.Movies.Movie 
            { 
                ImdbID = movieDto.imdbID,
                ImdbRating = movieDto.imdbRating,
                Title = movieDto.Title,
                Year = movieDto.Year,
                Genre = movieDto.Genre,
                Director = movieDto.Director,
                Writer = movieDto.Writer,
                Actors = movieDto.Actors,
                Videos = movieVideoDtos.Select(v => new MovieVideo
                {
                    ThumbUrl = v.ThumbUrl,
                    VideoUrl = v.VideoUrl
                })
            };
            
            // save the data in cache for 1 day
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            await _cache.SetStringAsync(request.Title, JsonSerializer.Serialize(movie), options);
        }
        else
        {
            movie = JsonSerializer.Deserialize<Domain.Movies.Movie>(cachedResponse);
        }

        var movieResponseDto = movie.ToDto();
        return movieResponseDto;

    }
}