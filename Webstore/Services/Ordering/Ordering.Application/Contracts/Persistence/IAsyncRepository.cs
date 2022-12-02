using System.Linq.Expressions;
using Ordering.Domain.Common;

namespace Ordering.Application.Contracts.Persistence;

public interface IAsyncRepository<T> where T : AggregateRoot
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Predicate<T>> predicate);
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Predicate<T>> predicate,
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy,
        string? includeString = null,
        bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(
        Expression<Predicate<T>> predicate,
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>? orderBy,
        List<Expression<Func<T, object>>> includes,
        bool disableTracking = true);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}