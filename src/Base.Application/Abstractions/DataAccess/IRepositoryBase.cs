using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Application.Abstractions.DataAccess
{
    public interface IRepositoryBase<TEntity, TDbContext> : IReadRepositoryBase<TEntity>
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity);

        Task UpdateAsync(TEntity entity);

        Task UpdateAsync<TId>(TId id, TEntity entity);

        Task UpdateRangeAsync(IEnumerable<TEntity> entity);

        Task DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(IEnumerable<TEntity> entity);
    }
}
