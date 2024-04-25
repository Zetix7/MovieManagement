using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MovieManagement.DataAccess;

public class MovieManagementStorageContextFactory : IDesignTimeDbContextFactory<MovieManagementStorageContext>
{
    public MovieManagementStorageContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<MovieManagementStorageContext>();
        optionBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MovieManagementStorage;Integrated Security=True;Encrypt=False");
        return new MovieManagementStorageContext(optionBuilder.Options);
    }
}
