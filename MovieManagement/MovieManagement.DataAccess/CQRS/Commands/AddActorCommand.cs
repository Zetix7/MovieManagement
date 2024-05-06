using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class AddActorCommand : CommandBase<Actor, Actor>
{
    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        var actor = await context.Actors.FirstOrDefaultAsync(x=>x.FirstName == Parameter!.FirstName && x.LastName == Parameter!.LastName);
        if (actor is not null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }
        await context.Actors.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
