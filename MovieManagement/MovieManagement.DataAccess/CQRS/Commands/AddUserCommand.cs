using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class AddUserCommand : CommandBase<User, User>
{
    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Login == Parameter!.Login);
        if (user is not null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        await context.Users.AddAsync(Parameter!);
        await context.SaveChangesAsync();
        return Parameter!;
    }
}
