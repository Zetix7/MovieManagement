using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class AddActorCommand : CommandBase<Actor, Actor>
{
    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        await context.Actors.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
