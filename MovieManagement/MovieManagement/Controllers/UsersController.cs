using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

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
        _logger.LogInformation("We are in GetAllUsers method - EndPoint GET");
        return await HandleRequest<GetUsersRequest, GetUsersResponse>(request);
    }

    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> GetMe([FromQuery] GetMeRequest request)
    {
        _logger.LogInformation("We are in GetMe method - EndPoint GET");
        return await HandleRequest<GetMeRequest, GetMeResponse>(request);
    }

    [HttpGet]
    [Route("{username}")]
    public async Task<IActionResult> GetUserByLogin([FromRoute] string username, GetUserByUsernameRequest request)
    {
        _logger.LogInformation("We are in GetUserByLogin method - EndPoint GET");
        request.Username = username;
        return await HandleRequest<GetUserByUsernameRequest, GetUserByUsernameResponse>(request);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        _logger.LogInformation("We are in AddUser method - EndPoint POST");
        return await HandleRequest<AddUserRequest, AddUserResponse>(request);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateUserByLogin([FromBody] UpdateUserByUsernameRequest request)
    {
        _logger.LogInformation("We are in UpdateUserByLogin method - EndPoint PUT");
        return await HandleRequest<UpdateUserByUsernameRequest, UpdateUserByUsernameResponse>(request);
    }

    [HttpPut]
    [Route("new-password")]
    public async Task<IActionResult> UpdateMyPassword([FromBody] UpdateMyPasswordRequest request)
    {
        _logger.LogInformation("We are in UpdateMyPassword method - EndPoint PUT");
        return await HandleRequest<UpdateMyPasswordRequest, UpdateMyPasswordResponse>(request);
    }

    [HttpGet]
    [Route("export")]
    public async Task<IActionResult> ExportUsersXmlFile(ExportUsersXmlFileRequest request)
    {
        _logger.LogInformation("We are in ExportUsersXmlFile method - EndPoint GET");
        return await HandleRequest<ExportUsersXmlFileRequest, ExportUsersXmlFileResponse>(request);
    }
}
