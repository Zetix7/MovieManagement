using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.DataAccess;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IRepository<Movie> _movieRepository;
    private readonly IMediator _mediator;

    public MovieController(IRepository<Movie> movieRepository, IMediator mediator)
    {
        _movieRepository = movieRepository;
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
