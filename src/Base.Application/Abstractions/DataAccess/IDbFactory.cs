using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Base.Application.Abstractions.DataAccess
{
    public interface IDbFactory
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //Task<int> SaveChangesWithLogsAsync(CancellationToken cancellationToken = default);

        ChangeTracker ChangeTracker { get; }

        DatabaseFacade Database { get; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();
    }
}
