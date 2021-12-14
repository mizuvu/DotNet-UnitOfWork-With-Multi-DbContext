using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Abstractions.DataAccess
{
    public interface IReadRepositoryBase<TEntity>
    {
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null);

        IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector);

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity> GetByKeyAsync(params object[] keys);

        Task<TEntity> GetByKeyAsync<TId>(TId id);

        Task<List<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default);
    }
}
