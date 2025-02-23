

using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies
{
    public static class ClientesDependency
    {
        public static void AddClientesDependency(this IServiceCollection services)
        {
            services.AddScoped<IClientesRepository, ClientesRepository>();
        }
    }
}
