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
    public async Task<IActionResult> GetActorById([FromRoute] int actorId)
    {
        var request = new GetActorByIdRequest { Id = actorId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddActor([FromBody] AddActorRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateActorById([FromBody] UpdateActorByIdRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{actorId}")]
    public async Task<IActionResult> RemoveActorById([FromRoute] int actorId)
    {
        var request = new RemoveActorByIdRequest { Id = actorId };
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}
