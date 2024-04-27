using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllMovies([FromQuery] GetMoviesRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("{movieId}")]
    public async Task<IActionResult> GetMovieById(int movieId)
    {
        var request = new GetMovieByIdRequest() { Id = movieId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
