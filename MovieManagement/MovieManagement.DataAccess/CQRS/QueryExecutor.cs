using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.DataAccess.CQRS;

public class QueryExecutor : IQueryExecutor
{
    private readonly MovieManagementStorageContext _context;

    public QueryExecutor(MovieManagementStorageContext context)
    {
        _context = context;
    }

    public Task<TResult> Execute<TResult>(QueryBase<TResult> query)
    {
        return query.Execute(_context);
    }
}
