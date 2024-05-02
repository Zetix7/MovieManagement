using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class RemoveActorByIdRequest : IRequest<RemoveActorByIdResponse>
{
    public int Id { get; set; }
}
