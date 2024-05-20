using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess.CQRS.Commands;

public class UpdateUserByUsernameCommand : CommandBase<User, User>
{
    public override async Task<User> Execute(MovieManagementStorageContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == Parameter!.Username);
        if (user == null)
        {
            Parameter!.Id = 0;
            return Parameter;
        }

        if (Parameter!.FirstName != null)
        {
            user.FirstName = Parameter.FirstName;
        }

        if (Parameter!.LastName != null)
        {
            user.LastName = Parameter.LastName;
        }

        if (Parameter!.AccessLevel != User.Role.None)
        {
            user.AccessLevel = Parameter.AccessLevel;
        }

        if (Parameter!.IsActive != user.IsActive)
        {
            user.IsActive = Parameter.IsActive;
        }

        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user;
    }
}
