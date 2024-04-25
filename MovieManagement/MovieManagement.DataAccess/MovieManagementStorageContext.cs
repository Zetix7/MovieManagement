using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess;

public class MovieManagementStorageContext : DbContext
{
    public MovieManagementStorageContext(DbContextOptions<MovieManagementStorageContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
}
