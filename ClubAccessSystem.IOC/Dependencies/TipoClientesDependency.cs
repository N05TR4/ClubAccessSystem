

using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies
{
    public static class TipoClientesDependency
    {
        public static void AddTipoClientesDependency(this IServiceCollection services)
        {
            services.AddScoped<ITipoClientesRepositorycs, TipoClientesRepository>();
        }
    }
}
