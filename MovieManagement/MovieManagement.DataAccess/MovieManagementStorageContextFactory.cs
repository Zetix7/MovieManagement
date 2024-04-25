using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieManagement.DataAccess;

public class MovieManagementStorageContextFactory : IDesignTimeDbContextFactory<MovieManagementStorageContext>
{
    public MovieManagementStorageContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MovieManagementStorageContext>();
        optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieManagementStorage;Integrated Security=True;Encrypt=False");
        return new MovieManagementStorageContext(optionsBuilder.Options);
    }
}
