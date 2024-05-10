using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetCurrentWeatherByCityRequest : IRequest<GetCurrentWeatherByCityResponse>
{
    public string? City { get; set; }
}
