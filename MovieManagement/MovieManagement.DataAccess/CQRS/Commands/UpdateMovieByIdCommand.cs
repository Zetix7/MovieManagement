using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateMovieByIdCommand : CommandBase<Movie, Movie>
{
    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(x=> x.Id == Parameter!.Id);
        if(movie is null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        movie.Title = Parameter!.Title;
        movie.Year = Parameter!.Year;
        movie.Universe = Parameter!.Universe;
        movie.BoxOffice = Parameter!.BoxOffice;
        context.Movies.Update(movie!);
        await context.SaveChangesAsync();
        return movie;
    }
}
