using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.OpenWeather;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetCurrentWeatherByCityHandler : IRequestHandler<GetCurrentWeatherByCityRequest, GetCurrentWeatherByCityResponse>
{
    private readonly ILogger<GetCurrentWeatherByCityHandler> _logger;
    private readonly IOpenWeatherConnector _openWeatherConnector;

    public GetCurrentWeatherByCityHandler(ILogger<GetCurrentWeatherByCityHandler> logger, IOpenWeatherConnector openWeatherConnector)
    {
        _logger = logger;
        _logger.LogInformation("We are in GetCurrentWeatherByCityHandler class");
        _openWeatherConnector = openWeatherConnector;
    }

    public async Task<GetCurrentWeatherByCityResponse> Handle(GetCurrentWeatherByCityRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in GetCurrentWeatherByCityHandler class");

        if (!request.IsActiveAuthentication)
        {
            return new GetCurrentWeatherByCityResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var city = request.City;
        var weather = await _openWeatherConnector.Connect(city!);
        if (weather is null)
        {
            return new GetCurrentWeatherByCityResponse { Error = new ErrorModel(ErrorType.NotFound)};
        }

        var response = new GetCurrentWeatherByCityResponse { Data = weather };
        return response;
    }
}
