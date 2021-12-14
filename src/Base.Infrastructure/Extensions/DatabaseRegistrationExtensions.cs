using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.Extensions
{
    public static class DatabaseRegistrationExtensions
    {
        public static IServiceCollection AddDatabaseContext<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            return services.AddMSSQL<TContext>(connectionString);
        }

        public static IServiceCollection AddMSSQL<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            services
                .AddDbContext<TContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection AddSQLite<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            services
                .AddDbContext<TContext>(options => options.UseSqlite(connectionString));

            return services;
        }
    }
}
