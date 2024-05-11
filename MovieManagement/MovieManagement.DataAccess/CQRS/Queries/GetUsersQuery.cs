using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetUsersQuery : QueryBase<List<User>>
{
    public override async Task<List<User>> Execute(MovieManagementStorageContext context)
    {
        return await context.Users.ToListAsync();
    }
}
