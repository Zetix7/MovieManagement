using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateActorByIdCommand : CommandBase<Actor, Actor>
{
    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        var actor = await context.Actors.Include(x => x.Movies).FirstOrDefaultAsync(x => x.Id == Parameter!.Id);

        if (actor == null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        if (Parameter!.FirstName is not null)
        {
            actor.FirstName = Parameter!.FirstName;
        }

        if (Parameter!.LastName is not null)
        {
            actor.LastName = Parameter!.LastName;
        }

        if (Parameter!.Movies is not null)
        {
            var actorMovies = actor.Movies!.ToList();
            var movies = Parameter!.Movies.Select(x => x.Id).ToList();
            foreach (var id in movies)
            {
                if (actorMovies.Find(x => x.Id == id) != null || context.Movies.FirstOrDefault(x=>x.Id == id) is null)
                {
                    continue;
                }
                actorMovies.Add(context.Movies.FirstOrDefault(x => x.Id == id)!);
            }

            actor.Movies = actorMovies;
        }

        context.Actors.Update(actor);
        await context.SaveChangesAsync();
        return actor;
    }
}
