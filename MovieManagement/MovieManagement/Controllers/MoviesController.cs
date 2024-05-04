﻿using MediatR;
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
    public async Task<IActionResult> GetMovieById([FromRoute] int movieId)
    {
        var request = new GetMovieByIdRequest() { Id = movieId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState
                .Where(x => x.Value!.Errors.Any())
                .Select(x => new { property = x.Key, errors = x.Value!.Errors }));
        }

        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateMovieById([FromBody] UpdateMovieByIdRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{movieId}")]
    public async Task<IActionResult> RemoveMovieById([FromRoute] int movieId)
    {
        var request = new RemoveMovieByIdRequest() { Id = movieId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
