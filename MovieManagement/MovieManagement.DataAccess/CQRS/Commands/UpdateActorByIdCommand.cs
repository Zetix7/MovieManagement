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
            actor.Movies = Parameter!.Movies;
        }

        context.Actors.Update(actor);
        await context.SaveChangesAsync();
        return actor;
    }
}
