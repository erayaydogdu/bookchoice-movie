using BookChoice.Movie.Application.Common;
using BookChoice.Movie.Application.Movies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookChoice.Movie.Api.Controllers;

[Route("api/movie")]
public class MovieController : BaseApiController
{
    [HttpPost("search")]
    public Task<MovieResponseDto> SearchAsync(SearchMovieRequest request)
    {
        return Mediator.Send(request);
    }
}