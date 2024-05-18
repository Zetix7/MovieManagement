using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class RemoveActorByIdRequest : RequestBase, IRequest<RemoveActorByIdResponse>
{
    public int Id { get; set; }
}
