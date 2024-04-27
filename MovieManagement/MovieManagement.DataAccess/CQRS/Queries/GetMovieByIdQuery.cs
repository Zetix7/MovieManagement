using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetMovieByIdQuery : QueryBase<Movie>
{
    public int Id { get; set; }

    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        var movie = await context.Movies.SingleOrDefaultAsync(m=>m.Id == Id);
        return movie!;
    }
}
