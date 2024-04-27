using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.DataAccess.CQRS;

public interface IQueryExecutor
{
    Task<TResult> Execute<TResult>(QueryBase<TResult> query);
}
