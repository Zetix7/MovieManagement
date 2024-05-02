using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetActorsQuery : QueryBase<List<Actor>>
{
    public string? LastName { get; set; }

    public override async Task<List<Actor>> Execute(MovieManagementStorageContext context)
    {
        return LastName == null ? await context.Actors.ToListAsync() : await context.Actors.Where(x=>x.LastName == LastName).ToListAsync();
    }
}
