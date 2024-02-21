namespace BookChoice.Movie.Application.Movies;

public class MovieResponseDto
{
    public string imdbID { get; set; }
    public string imdbRating { get; set; } 
    public string Title { get; set; } 
    public string Year { get; set; } 
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Writer { get; set; } 
    public string Actors { get; set; } 
    public IEnumerable<MovieVideosDto> Videos { get; set; }
}