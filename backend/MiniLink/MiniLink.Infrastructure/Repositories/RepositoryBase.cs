using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MiniLink.Domain.Repositories;

namespace MiniLink.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> Insert(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
}