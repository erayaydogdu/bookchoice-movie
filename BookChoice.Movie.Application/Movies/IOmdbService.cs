namespace BookChoice.Movie.Application.Movies;

public interface IOmdbService
{
    Task<MovieDto> GetMovieDetailsAsync(string movieName);
}