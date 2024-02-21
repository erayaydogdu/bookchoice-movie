using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace BookChoice.Movie.Api.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;
    
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}