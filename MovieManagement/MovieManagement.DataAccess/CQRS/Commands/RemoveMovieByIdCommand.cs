using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class RemoveMovieByIdCommand : CommandBase<Movie, Movie>
{
    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(x=>x.Id == Parameter!.Id);

        if(movie is null)
        {
            return Parameter!;
        }

        context.Movies.Remove(movie!);
        await context.SaveChangesAsync();
        return movie!;
    }
}
