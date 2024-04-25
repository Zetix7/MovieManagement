
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

    public IEnumerable<T> GetAll()
    {
        return _entities.AsEnumerable();
    }

    public T GetById(int id)
    {
        return _entities.SingleOrDefault(e => e.Id == id)!;
    }

    public void Insert(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _entities.Update(entity);
        _context.SaveChanges();
    }
    public void Delete(int id)
    {
        T entityToDelete = _entities.SingleOrDefault(e => e.Id == id)!;
        if (entityToDelete is null)
        {
            throw new ArgumentNullException(nameof(entityToDelete));
        }

        _entities.Remove(entityToDelete);
        _context.SaveChanges();
    }

}
