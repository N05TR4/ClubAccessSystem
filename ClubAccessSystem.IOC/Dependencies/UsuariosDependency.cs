

using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies
{
    public static class UsuariosDependency
    {
        public static void AddUsuariosDependency(this IServiceCollection services)
        {
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
        }
    }
}
