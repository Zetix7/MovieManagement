using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetMoviesQuery : QueryBase<List<Movie>>
{
    public override async Task<List<Movie>> Execute(MovieManagementStorageContext context)
    {
        return await context.Movies.ToListAsync();
    }
}
