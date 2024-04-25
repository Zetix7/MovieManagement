namespace MovieManagement.ApplicationServices.API.Domain.Models;

public class Movie
{
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }
}
