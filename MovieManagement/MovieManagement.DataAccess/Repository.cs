
using Microsoft.EntityFrameworkCore;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.DataAccess;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    protected readonly MovieManagementStorageContext _context;
    private readonly DbSet<T> _entities;

    public Repository(MovieManagementStorageContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public Task<List<T>> GetAll()
    {
        return _entities.ToListAsync();
    }

    public Task<T> GetById(int id)
    {
        return _entities.SingleOrDefaultAsync(e => e.Id == id)!;
    }

    public async Task Insert(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Delete(int id)
    {
        T entityToDelete = _entities.SingleOrDefault(e => e.Id == id)!;
        if (entityToDelete is null)
        {
            throw new ArgumentNullException(nameof(entityToDelete));
        }

        _entities.Remove(entityToDelete);
        await _context.SaveChangesAsync();
    }

}
