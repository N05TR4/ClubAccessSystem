
using AutoMapper;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Base;
using ClubAccessSystem.Persistence.Context;
using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Repositories
{
    public class TipoClientesRepository : BaseRepository<TipoClientes, TipoClientesModels>, ITipoClientesRepositorycs
    {
        private readonly ClubContext _dbContext;

        public TipoClientesRepository(ClubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }
    }
}
