using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateActorByIdCommand : CommandBase<Actor, Actor>
{
    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        var actor = await context.Actors.FirstOrDefaultAsync(x=>x.Id == Parameter!.Id);

        if (actor == null)
        {
            return Parameter!;
        }

        actor.FirstName = Parameter!.FirstName;
        actor.LastName = Parameter!.LastName;
        context.Actors.Update(actor);
        await context.SaveChangesAsync();
        return actor;
    }
}
