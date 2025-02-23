

using ClubAccessSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClubAccessSystem.Persistence.Context
{
    public partial class ClubContext : DbContext
    {
        public ClubContext(DbContextOptions<ClubContext> options) : base(options)
        {

        }

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<RegistrosAcceso> RegistrosAcceso { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<TipoClientes> TipoClientes { get; set; }
    }
}
