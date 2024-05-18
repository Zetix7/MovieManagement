using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class RemoveMovieByIdRequest : RequestBase, IRequest<RemoveMovieByIdResponse>
{
    public int Id { get; set; }
}
