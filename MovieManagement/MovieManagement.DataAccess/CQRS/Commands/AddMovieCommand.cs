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

        if(Parameter!.Actors is not null)
        {
            var movieCast = Parameter.Actors.Select(x=>x.Id).ToList();
            var cast = new List<Actor>();
            foreach (var id in movieCast)
            {
                if (context.Actors.FirstOrDefault(x => x.Id == id) is null)
                {
                    continue;
                }
                cast.Add(context.Actors.FirstOrDefault(x => x.Id == id)!);
            }
            Parameter!.Actors = cast;
        }

        await context.Movies.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
