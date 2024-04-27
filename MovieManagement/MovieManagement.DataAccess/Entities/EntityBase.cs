using System.ComponentModel.DataAnnotations;

namespace MovieManagement.DataAccess.Entities;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}
