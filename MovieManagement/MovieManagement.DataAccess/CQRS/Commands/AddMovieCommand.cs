using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class AddMovieCommand : CommandBase<Movie, Movie>
{
    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(x => x.Title == Parameter!.Title && x.Year == Parameter!.Year);
        if(movie is not null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        await context.Movies.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
