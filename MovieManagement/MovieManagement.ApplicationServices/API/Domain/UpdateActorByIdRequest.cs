using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class UpdateActorByIdRequest : IRequest<UpdateActorByIdResponse>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
