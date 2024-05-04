using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

public class MoviesController : ApiControllerBase
{
    public MoviesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllMovies([FromQuery] GetMoviesRequest request)
    {
        return await HandleRequest<GetMoviesRequest, GetMoviesResponse>(request);
    }

    [HttpGet]
    [Route("{movieId}")]
    public async Task<IActionResult> GetMovieById([FromRoute] int movieId)
    {
        var request = new GetMovieByIdRequest() { Id = movieId };
        return await HandleRequest<GetMovieByIdRequest, GetMovieByIdResponse>(request);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
    {
        return await HandleRequest<AddMovieRequest, AddMovieResponse>(request);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateMovieById([FromBody] UpdateMovieByIdRequest request)
    {
        return await HandleRequest<UpdateMovieByIdRequest, UpdateMovieByIdResponse>(request);
    }

    [HttpDelete]
    [Route("{movieId}")]
    public async Task<IActionResult> RemoveMovieById([FromRoute] int movieId)
    {
        var request = new RemoveMovieByIdRequest() { Id = movieId };
        return await HandleRequest<RemoveMovieByIdRequest, RemoveMovieByIdResponse>(request);
    }
}
