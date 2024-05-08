namespace MovieManagement.ApplicationServices.API.Domain.Models;

public class Actor
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public List<string>? MovieTitleList { get; set; }
}
