using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.DAL;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BugTrackingSystemContext _context;
    public GenericRepository(BugTrackingSystemContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>()
            .AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Set<T>()
            .FindAsync(id) ?? null;
        _context.Set<T>()
            .Remove(entity);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await _context.Set<T>()
            .FindAsync(id) ?? null;
        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
    }
}
