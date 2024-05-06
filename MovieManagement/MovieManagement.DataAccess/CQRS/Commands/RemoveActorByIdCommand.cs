using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class RemoveActorByIdCommand : CommandBase<Actor, Actor>
{
    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        var actor = await context.Actors.FirstOrDefaultAsync(x => x.Id == Parameter!.Id);
        if (actor is null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        context.Actors.Remove(actor);
        await context.SaveChangesAsync();
        return actor;
    }
}
