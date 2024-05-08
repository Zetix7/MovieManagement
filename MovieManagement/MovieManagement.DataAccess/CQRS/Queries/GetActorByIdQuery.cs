using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetActorByIdQuery : QueryBase<Actor>
{
    public int Id { get; set; }

    public override async Task<Actor> Execute(MovieManagementStorageContext context)
    {
        var actor = await context.Actors.Include(x => x.Movies).SingleOrDefaultAsync(a => a.Id == Id);
        return actor!;
    }
}
