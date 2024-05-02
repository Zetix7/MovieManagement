using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetMoviesRequest : IRequest<GetMoviesResponse>
{
    public string? Title { get; set; }
}
