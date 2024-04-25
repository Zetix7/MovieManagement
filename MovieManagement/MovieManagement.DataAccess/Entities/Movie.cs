using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MovieManagement.DataAccess.Entities;

public class Movie : EntityBase
{
    [MaxLength(200)]
    public string? Title { get; set; }

    public int Year { get; set; }

    [MaxLength(100)]
    public string? Universe { get; set; }

    [Precision(17,2)]
    public decimal BoxOffice { get; set; }
}
