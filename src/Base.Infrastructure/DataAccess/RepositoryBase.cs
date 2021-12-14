using Base.Application.Abstractions.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Infrastructure.DataAccess
{
    public class RepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity, TDbContext>
        where TEntity : class
        where TDbContext : IDbFactory
    {
        private readonly TDbContext _dbContext;

        public RepositoryBase(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region[Read Repository]
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _dbContext.Set<TEntity>().Select(selector);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return _dbContext.Set<TEntity>();
            }
            else
            {
                return _dbContext.Set<TEntity>().Where(expression);
            }
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByKeyAsync(params object[] keys)
        {
            var data = await _dbContext.Set<TEntity>().FindAsync(keys);
            //if (data == null) throw new EntityNotFoundException($"{keys} not found.");
            return data;
        }

        public virtual async Task<TEntity> GetByKeyAsync<TId>(TId id)
        {
            var data = await _dbContext.Set<TEntity>().FindAsync(id);
            //if (data == null) throw new EntityNotFoundException($"{id} not found.");
            return data;
        }

        public virtual async Task<List<TResult>> ToListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>()
                .Select(selector)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            return await query.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            return await query.AnyAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            return await query.CountAsync(cancellationToken);
        }
        #endregion

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entity);
            return entity;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync<TId>(TId id, TEntity entity)
        {
            TEntity exist = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            _dbContext.Set<TEntity>().UpdateRange(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entity)
        {
            _dbContext.Set<TEntity>().RemoveRange(entity);
            return Task.CompletedTask;
        }
    }
}
