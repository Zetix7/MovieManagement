using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

[Authorize]
public class UsersController : ApiControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(mediator, logger)
    {
        _logger = logger;
        _logger.LogInformation("We are in UsersController class");
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersRequest request)
    {
        return await HandleRequest<GetUsersRequest, GetUsersResponse>(request);
    }
}
