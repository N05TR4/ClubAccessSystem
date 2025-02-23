

using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies
{
    public static class RegistrosAccesoDependency
    {
        public static void AddRegistrosAccesoDependency(this IServiceCollection services)
        {
            services.AddScoped<IRegistrosAccesoRepository, RegistrosAccesoRepository>();
        }
    }
}
