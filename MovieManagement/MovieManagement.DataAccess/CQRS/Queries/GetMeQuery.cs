using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Queries;

public class GetMeQuery : QueryBase<User>
{
    public string? Login { get; set; }
    
    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Login == Login)!;
    }
}
