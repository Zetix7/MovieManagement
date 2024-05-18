using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetMoviesRequest : RequestBase, IRequest<GetMoviesResponse>
{
    public string? Title { get; set; }
}
