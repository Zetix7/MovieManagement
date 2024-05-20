namespace MovieManagement.ApplicationServices.API.Domain.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Universe { get; set; }
    public decimal BoxOffice { get; set; }
    public List<string>? Cast { get; set; }
}
