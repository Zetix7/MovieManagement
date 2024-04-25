using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetActorByIdRequest : IRequest<GetActorByIdResponse>
{
    public int Id { get; set; }
}
