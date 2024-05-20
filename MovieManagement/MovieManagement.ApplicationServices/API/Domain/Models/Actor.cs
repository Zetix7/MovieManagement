namespace MovieManagement.ApplicationServices.API.Domain.Models;

public class Actor
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public List<string>? MovieTitleList { get; set; }
}
