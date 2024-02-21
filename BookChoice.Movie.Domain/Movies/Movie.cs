namespace BookChoice.Movie.Domain.Movies;

public class Movie
{
    public string ImdbID { get; set; }
    public string ImdbRating { get; set; } 
    public string Title { get; set; }
    public string Year { get; set; }
    public string Genre { get; set; } 
    public string Director { get; set; } 
    public string Writer { get; set; } 
    public string Actors { get; set; }
    public IEnumerable<MovieVideo> Videos { get; set; }
    
}

