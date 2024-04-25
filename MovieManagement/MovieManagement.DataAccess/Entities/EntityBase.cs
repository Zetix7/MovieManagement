using System.ComponentModel.DataAnnotations;

namespace MovieManagement.DataAccess.Entities;

public class EntityBase
{
    [Key]
    public int Id { get; set; }
}
