using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class RemoveMovieByIdRequest : IRequest<RemoveMovieByIdResponse>
{
    public int Id { get; set; }
}
