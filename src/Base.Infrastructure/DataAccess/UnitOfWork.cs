using Base.Application.Abstractions.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Infrastructure.DataAccess
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
        where TDbContext : IDbFactory
    {
        private readonly TDbContext _dbContext;
        private bool disposed;
        private IDbContextTransaction _currentTransaction;
        private Hashtable _repositories;


        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IRepositoryBase<TEntity, TDbContext> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<,>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TDbContext)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepositoryBase<TEntity, TDbContext>)_repositories[type];
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public virtual async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken: cancellationToken);
        }

        public virtual async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);

                _currentTransaction?.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public virtual async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _currentTransaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public virtual Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public virtual async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<int> SaveWithLogsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
            //return await _dbContext.SaveChangesWithLogsAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
    }
}
