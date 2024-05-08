using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateMovieByIdCommand : CommandBase<Movie, Movie>
{
    public override async Task<Movie> Execute(MovieManagementStorageContext context)
    {
        var movie = await context.Movies.Include(x=>x.Actors).FirstOrDefaultAsync(x => x.Id == Parameter!.Id);
        if (movie is null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        if (Parameter!.Title is not null)
        {
            movie.Title = Parameter.Title;
        }

        if (Parameter!.Year is not 0)
        {
            movie.Year = Parameter.Year;
        }

        if (Parameter!.Universe is not null)
        {
            movie.Universe = Parameter.Universe;
        }

        if (Parameter!.BoxOffice is not 0)
        {
            movie.BoxOffice = Parameter.BoxOffice;
        }

        if (Parameter!.Actors is not null)
        {
            var movieCast = movie.Actors!.ToList();
            var castIds = Parameter.Actors.Select(x => x.Id).ToList();
            foreach (var id in castIds)
            {
                if (movieCast.Find(x => x.Id == id) != null)
                {
                    continue;
                }
                movieCast.Add(context.Actors.FirstOrDefault(x => x.Id == id)!);
            }

            movie.Actors = movieCast;
        }

        context.Movies.Update(movie!);
        await context.SaveChangesAsync();
        return movie;
    }
}
