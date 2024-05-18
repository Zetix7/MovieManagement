using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class UpdateMovieByIdRequest : RequestBase, IRequest<UpdateMovieByIdResponse>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }
    public List<int>? CastIds { get; set; }
}
