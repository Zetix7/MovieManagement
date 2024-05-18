using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetMovieByIdRequest : RequestBase, IRequest<GetMovieByIdResponse>
{
    public int Id { get; set; }
}
