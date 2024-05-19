using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetUserByLoginQuery : QueryBase<User>
{
    public string? Login { get; set; }

    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == Login);
        return user!;
    }
}
