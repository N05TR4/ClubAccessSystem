

using AutoMapper;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Base;
using ClubAccessSystem.Persistence.Context;
using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Repositories
{
    public class RolesRepository : BaseRepository<Roles, RolesModels>, IRolesRepository
    {
        private readonly ClubContext _dbContext;

        public RolesRepository(ClubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }
    }
}
