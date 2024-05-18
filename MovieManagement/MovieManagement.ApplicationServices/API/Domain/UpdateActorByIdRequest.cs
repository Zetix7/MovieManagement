using MediatR;
using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.API.Domain;

public class UpdateActorByIdRequest : RequestBase, IRequest<UpdateActorByIdResponse>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<int>? MovieListIds{ get; set; }
}
