using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateMyPasswordCommand : CommandBase<User,User>
{
    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == Parameter!.Username);
        if(user is null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        user.Password = Parameter!.Password;
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }
}
