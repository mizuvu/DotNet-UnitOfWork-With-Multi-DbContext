using Base.Application.Abstractions.DataAccess;
using Base.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            return services;
        }
    }
}
