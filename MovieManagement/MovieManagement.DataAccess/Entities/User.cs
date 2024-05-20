using System.ComponentModel.DataAnnotations;

namespace MovieManagement.DataAccess.Entities;

public class User : EntityBase
{
    public enum Role
    {
        None = 0,
        UserService = 1,
        AdministratorService = 2,
    }

    [StringLength(20, MinimumLength = 1)]
    public string? FirstName { get; set; }

    [StringLength(50, MinimumLength = 1)]
    public string? LastName { get; set; }
    
    [Required]    
    [StringLength(15, MinimumLength = 8)]
    public string? Username { get; set; }

    [Required]
    [StringLength(69)]
    public string? Password { get; set; }

    [Required]
    public Role AccessLevel { get; set; }

    [Required]    
    public bool IsActive { get; set; }
}
