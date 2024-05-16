using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetUSerQuery : QueryBase<User>
{
    public string? Username { get; set; }

    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        return await context.Users.FirstOrDefaultAsync(x=>x.Username == Username);
    }
}
