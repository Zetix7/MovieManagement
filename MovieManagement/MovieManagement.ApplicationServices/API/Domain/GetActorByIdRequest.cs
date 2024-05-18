using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetActorByIdRequest : RequestBase, IRequest<GetActorByIdResponse>
{
    public int Id { get; set; }
}
