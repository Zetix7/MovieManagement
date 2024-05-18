using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class AddActorRequest : RequestBase, IRequest<AddActorResponse>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<int>? MovieListIds{ get; set; }
}
