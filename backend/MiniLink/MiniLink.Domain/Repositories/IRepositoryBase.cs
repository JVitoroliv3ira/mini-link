using System.Linq.Expressions;

namespace MiniLink.Domain.Repositories;

public interface IRepositoryBase<T> where T : class
{
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate);
    Task<int> Count(Expression<Func<T, bool>> predicate);
    Task<bool> Exists(Expression<Func<T, bool>> predicate);
}