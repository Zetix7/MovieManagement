using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetUserQuery : QueryBase<User>
{
    public string? Username { get; set; }

    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Login == Username);
    }
}
