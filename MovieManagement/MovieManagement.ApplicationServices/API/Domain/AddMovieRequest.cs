using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class AddMovieRequest : IRequest<AddMovieResponse>
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }
}
