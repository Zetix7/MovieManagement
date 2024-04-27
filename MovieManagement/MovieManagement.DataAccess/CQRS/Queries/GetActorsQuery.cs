using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetActorsQuery : QueryBase<List<Actor>>
{
    public override async Task<List<Actor>> Execute(MovieManagementStorageContext context)
    {
        return await context.Actors.ToListAsync();
    }
}
