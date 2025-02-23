using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies
{
    public static class RolesDependency
    {
        public static void AddRolesDependency(this IServiceCollection services)
        {
            services.AddScoped<IRolesRepository, RolesRepository>();
        }
    }
}
