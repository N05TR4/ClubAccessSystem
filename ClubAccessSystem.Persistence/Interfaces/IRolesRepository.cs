

using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Domain.Repositories;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Interfaces
{
    public interface IRolesRepository : IBaseRepository<Roles, RolesModels>
    {
    }
}
