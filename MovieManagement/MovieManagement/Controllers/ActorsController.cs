using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

public class ActorsController : ApiControllerBase
{
    private readonly ILogger<ActorsController> _logger;

    public ActorsController(IMediator mediator, ILogger<ActorsController> logger) : base(mediator, logger)
    {
        _logger = logger;
        _logger.LogInformation("We are in ActorsController controller class");
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetActors([FromQuery] GetActorsRequest request)
    {
        _logger.LogInformation("We are in GetActors method - EndPoint GET");
        return await HandleRequest<GetActorsRequest, GetActorsResponse>(request);
    }

    [HttpGet]
    [Route("{actorId}")]
    public async Task<IActionResult> GetActorById([FromRoute] int actorId)
    {
        _logger.LogInformation("We are in GetActorById method - EndPoint GET");
        var request = new GetActorByIdRequest { Id = actorId };
        return await HandleRequest<GetActorByIdRequest, GetActorByIdResponse>(request);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddActor([FromBody] AddActorRequest request)
    {
        _logger.LogInformation("We are in AddActor method - EndPoint POST");
        return await HandleRequest<AddActorRequest, AddActorResponse>(request);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateActorById([FromBody] UpdateActorByIdRequest request)
    {
        _logger.LogInformation("We are in UpdateActorById method - EndPoint PUT");
        return await HandleRequest<UpdateActorByIdRequest, UpdateActorByIdResponse>(request);
    }

    [HttpDelete]
    [Route("{actorId}")]
    public async Task<IActionResult> RemoveActorById([FromRoute] int actorId)
    {
        _logger.LogInformation("We are in DeleteActorById method - EndPoint DELETE");
        var request = new RemoveActorByIdRequest { Id = actorId };
        return await HandleRequest<RemoveActorByIdRequest, RemoveActorByIdResponse>(request);
    }

    [HttpGet]
    [Route("export")]
    public async Task<IActionResult> ExportActorsXmlFile([FromQuery] ExportActorsXmlFileRequest request)
    {
        _logger.LogInformation("We are in ExportActorsXmlFile method - EndPoint GET");
        return await HandleRequest<ExportActorsXmlFileRequest, ExportActorsXmlFileResponse>(request);
    }

    [HttpPost]
    [Route("import")]
    public async Task<IActionResult> ImportActorsXmlFile([FromQuery] ImportActorsXmlFileRequest request)
    {
        _logger.LogInformation("We are in ImportActorsXmlFile method - EndPoint POST");
        return await HandleRequest<ImportActorsXmlFileRequest, ImportActorsXmlFileResponse>(request);
    }
}
