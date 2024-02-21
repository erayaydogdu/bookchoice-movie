using BookChoice.Movie.Application.Movies;

namespace BookChoice.Movie.Application.Mapping;

public static class MovieMapper
{
    public static MovieResponseDto ToDto(this Domain.Movies.Movie movie)
    {
        return new MovieResponseDto
        {
            Actors = movie.Actors,
            Director = movie.Director,
            Genre = movie.Genre,
            imdbID = movie.ImdbID,
            imdbRating = movie.ImdbRating,
            Title = movie.Title,
            Year = movie.Year,
            Writer = movie.Writer,
            Videos = movie.Videos != null
                ? movie.Videos.Select(v => new MovieVideosDto
                {
                    ThumbUrl = v.ThumbUrl,
                    VideoUrl = v.VideoUrl
                })
                : Enumerable.Empty<MovieVideosDto>()
        };
    }
}