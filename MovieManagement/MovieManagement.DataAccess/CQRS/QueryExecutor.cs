using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.DataAccess.CQRS;

public class QueryExecutor : IQueryExecutor
{
    private readonly MovieManagementStorageContext _context;

    public QueryExecutor(MovieManagementStorageContext context)
    {
        _context = context;
    }

    public async Task<TResult> Execute<TResult>(QueryBase<TResult> query)
    {
        return await query.Execute(_context);
    }
}
