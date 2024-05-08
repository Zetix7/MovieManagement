using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetActorsQuery : QueryBase<List<Actor>>
{
    public string? LastName { get; set; }

    public override async Task<List<Actor>> Execute(MovieManagementStorageContext context)
    {
        return LastName == null
            ? await context.Actors.Include(x => x.Movies).ToListAsync()
            : await context.Actors.Include(x => x.Movies).Where(x => x.LastName == LastName).ToListAsync();
    }
}
