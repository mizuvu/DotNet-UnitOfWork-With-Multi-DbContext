using System;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Abstractions.DataAccess
{
    public interface IUnitOfWork<TDbContext> : IDisposable
    {
        IRepositoryBase<TEntity, TDbContext> Repository<TEntity>();

        bool HasActiveTransaction { get; }
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        Task Rollback();
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        Task<int> SaveWithLogsAsync(CancellationToken cancellationToken = default);
    }
}
