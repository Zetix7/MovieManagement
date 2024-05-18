﻿using MediatR;
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
    [Route("{login}")]
    public async Task<IActionResult> GetUserByLogin([FromRoute] string login, GetUserByLoginRequest request)
    {
        _logger.LogInformation("We are in GetUserByLogin method - EndPoint GET");
        request.Login = login;
        return await HandleRequest<GetUserByLoginRequest, GetUserByLoginResponse>(request);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        _logger.LogInformation("We are in AddUsers method - EndPoint POST");
        return await HandleRequest<AddUserRequest, AddUserResponse>(request);
    }
}
