using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetActorsRequest : IRequest<GetActorsResponse>
{
    public string? LastName { get; set; }
}
