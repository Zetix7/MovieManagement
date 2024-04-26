using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetActors([FromQuery] GetActorsRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet]
    [Route("{actorId}")]
    public async Task<IActionResult> GetActorById(int actorId)
    {
        var request = new GetActorByIdRequest { Id = actorId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
