using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.DataAccess.CQRS;

public class CommandExecutor : ICommandExecutor
{
    private readonly MovieManagementStorageContext _context;

    public CommandExecutor(MovieManagementStorageContext context)
    {
        _context = context;
    }

    public Task<TResult> Execute<TParameters, TResult>(CommandBase<TParameters,TResult> command)
    {
        return command.Execute(_context);
    }
}
