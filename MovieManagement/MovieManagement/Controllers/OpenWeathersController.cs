using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

public class OpenWeathersController : ApiControllerBase
{
    private readonly ILogger<OpenWeathersController> _logger;

    public OpenWeathersController(IMediator mediator, ILogger<OpenWeathersController> logger) : base(mediator, logger)
    {
        _logger = logger;
        _logger.LogInformation("We are in OpenWeathersController class");
    }

    [HttpGet]
    [Route("{city}")]
    public async Task<IActionResult> GetCurrentWeatherByCity([FromRoute] string city)
    {
        _logger.LogInformation("We are in GetCurrentWeatherByCity method - EndPoint GET");
        var request = new GetCurrentWeatherByCityRequest { City = city };
        var response = await HandleRequest<GetCurrentWeatherByCityRequest, GetCurrentWeatherByCityResponse>(request);
        return Ok(response);
    }
}
