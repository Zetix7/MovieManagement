using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

public class ActorsController : ApiControllerBase
{
    public ActorsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetActors([FromQuery] GetActorsRequest request)
    {
        return await HandleRequest<GetActorsRequest, GetActorsResponse>(request);
    }

    [HttpGet]
    [Route("{actorId}")]
    public async Task<IActionResult> GetActorById([FromRoute] int actorId)
    {
        var request = new GetActorByIdRequest { Id = actorId };
        return await HandleRequest<GetActorByIdRequest, GetActorByIdResponse>(request);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddActor([FromBody] AddActorRequest request)
    {
        return await HandleRequest<AddActorRequest, AddActorResponse>(request);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateActorById([FromBody] UpdateActorByIdRequest request)
    {
        return await HandleRequest<UpdateActorByIdRequest, UpdateActorByIdResponse>(request);
    }

    [HttpDelete]
    [Route("{actorId}")]
    public async Task<IActionResult> RemoveActorById([FromRoute] int actorId)
    {
        var request = new RemoveActorByIdRequest { Id = actorId };
        return await HandleRequest<RemoveActorByIdRequest, RemoveActorByIdResponse>(request);
    }
}
