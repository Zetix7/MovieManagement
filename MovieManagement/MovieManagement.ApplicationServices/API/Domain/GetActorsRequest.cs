using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetActorsRequest : RequestBase, IRequest<GetActorsResponse>
{
    public string? LastName { get; set; }
}
