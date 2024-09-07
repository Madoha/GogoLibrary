using GogoLibrary.Domain.Interfaces.Repositories;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.DAL.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(entity)} is null");

        await _context.AddAsync(entity);
        return entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(entity)} is null");

        _context.Update(entity);
        return Task.FromResult(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException($"{nameof(entity)} is null");

        _context.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}