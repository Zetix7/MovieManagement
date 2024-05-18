using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using System.Net;
using System.Security.Claims;

namespace MovieManagement.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ApiControllerBase> _logger;

    public ApiControllerBase(IMediator mediator, ILogger<ApiControllerBase> logger)
    {
        _mediator = mediator;
        _logger = logger;
        _logger.LogInformation("We are in ApiControllerBase class");
    }

    protected async Task<IActionResult> HandleRequest<TRequest, TResponse>(TRequest request)
        where TRequest : RequestBase, IRequest<TResponse>
        where TResponse : ErrorResponseBase
    {
        _logger.LogInformation("We are in HandleRequest method in ApiControllerBase class");
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState
                .Where(x => x.Value!.Errors.Any())
                .Select(x => new { property = x.Key, errors = x.Value!.Errors }));
        }

        if (User.Claims.FirstOrDefault() != null)
        {
            request.LoginAuthentication = User.FindFirstValue(ClaimTypes.Name);
            request.AccessLevelAuthentication = User.FindFirstValue(ClaimTypes.Role);
            request.IsActiveAuthentication = bool.Parse(User.FindFirstValue(ClaimTypes.UserData)!);
        }

        var response = await _mediator.Send(request);
        if (response.Error is not null)
        {
            return ErrorResponse(response.Error);
        }

        return Ok(response);
    }

    private IActionResult ErrorResponse(ErrorModel errorModel)
    {
        _logger.LogInformation("We are in ErrorResponse method in ApiControllerBase class");
        var httpCode = GetHttpStatusCode(errorModel.Error);
        return StatusCode((int)httpCode, errorModel);
    }

    private HttpStatusCode GetHttpStatusCode(string errorType)
    {
        _logger.LogInformation("We are in GetHttpStatusCode method in ApiControllerBase class");
        return errorType switch
        {
            ErrorType.InternalServerError => HttpStatusCode.InternalServerError,
            ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.UnsupportedMediaType => HttpStatusCode.UnsupportedMediaType,
            ErrorType.UnsupportedMethod => HttpStatusCode.MethodNotAllowed,
            ErrorType.RequestTooLarge => HttpStatusCode.RequestEntityTooLarge,
            ErrorType.TooManyRequests => HttpStatusCode.TooManyRequests,
            _ => HttpStatusCode.BadRequest
        };
    }
}