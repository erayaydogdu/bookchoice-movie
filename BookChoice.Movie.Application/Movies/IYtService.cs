namespace BookChoice.Movie.Application.Movies;

public interface IYtService
{
    Task<List<MovieVideosDto>> GetMovieVideosAsync(string movieName, int count);
}