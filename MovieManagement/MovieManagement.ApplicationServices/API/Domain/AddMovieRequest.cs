using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class AddMovieRequest : RequestBase, IRequest<AddMovieResponse>
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }
    public List<int>? CastIds { get; set; }
}
