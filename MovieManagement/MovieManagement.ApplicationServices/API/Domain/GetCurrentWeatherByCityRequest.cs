using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetCurrentWeatherByCityRequest : RequestBase, IRequest<GetCurrentWeatherByCityResponse>
{
    public string? City { get; set; }
}
