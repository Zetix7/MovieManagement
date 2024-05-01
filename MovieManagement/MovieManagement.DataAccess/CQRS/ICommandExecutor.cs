using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.DataAccess.CQRS;

public interface ICommandExecutor
{
    Task<TResult> Execute<TParameters, TResult>(CommandBase<TParameters, TResult> command);
}
