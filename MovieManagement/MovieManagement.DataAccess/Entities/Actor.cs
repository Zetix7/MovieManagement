using System.ComponentModel.DataAnnotations;

namespace MovieManagement.DataAccess.Entities;

public class Actor : EntityBase
{
    public List<Movie>? Movies { get; set; }

    [MaxLength(50)]
    public string? FirstName { get; set; }
    
    [MaxLength(100)]
    public string? LastName { get; set; }
}
 