

using Microsoft.Extensions.DependencyInjection;

namespace ClubAccessSystem.IOC.Dependencies.Configuration
{
    public static class ConfigurationDependency
    {
        public static void AddConfigurationDependency(this IServiceCollection services)
        {
            services.AddClientesDependency();
            services.AddRegistrosAccesoDependency();
            services.AddRolesDependency();
            services.AddTipoClientesDependency();
            services.AddUsuariosDependency();
        }
    }
}
