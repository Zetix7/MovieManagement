using static MovieManagement.DataAccess.Entities.User;

namespace MovieManagement.ApplicationServices.API.Domain.Models;

public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public Role AccessLevel { get; set; }
    public bool IsActive { get; set; }
}
