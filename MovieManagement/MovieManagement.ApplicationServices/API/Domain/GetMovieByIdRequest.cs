using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class GetMovieByIdRequest : IRequest<GetMovieByIdResponse>
{
    public int Id { get; set; }
}
