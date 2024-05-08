using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetMoviesQuery : QueryBase<List<Movie>>
{
    public string? Title { get; set; }

    public override async Task<List<Movie>> Execute(MovieManagementStorageContext context)
    {
        return Title == null 
            ? await context.Movies.Include(x => x.Actors).ToListAsync() 
            : await context.Movies.Include(x => x.Actors).Where(x=>x.Title == Title).ToListAsync();
    }
}
