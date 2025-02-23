

using AutoMapper;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Domain.Result;
using ClubAccessSystem.Persistence.Base;
using ClubAccessSystem.Persistence.Context;
using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ClubAccessSystem.Persistence.Repositories
{
    public class RegistrosAccesoRepository : BaseRepository<RegistrosAcceso, RegistrosAccesoModels>, IRegistrosAccesoRepository
    {
        private readonly ClubContext _dbContext;
        public RegistrosAccesoRepository(ClubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }


        public async Task<OperationResult<RegistrosAccesoModels>> GetClienteById(int clienteId)
        {
            if (clienteId <= 0) return new OperationResult<RegistrosAccesoModels> { Success = false, Message = "ID cannot be null or less than or equal to 0" };

            return await SafeExecuteAsync(async () =>
            {
                var registro = await _dbContext.RegistrosAcceso.Where(a => a.ClienteId == clienteId).ToListAsync();
                var registroDTO = registro.Select(a => new RegistrosAccesoModels
                {
                    RegistroId = a.RegistroId,
                    FechaEntrada = a.fechaEntrada,
                    FechaSalida = a.FechaSalida,
                    ClienteId = a.ClienteId,
                });

                return new OperationResult<RegistrosAccesoModels> { Success = true, Data = (RegistrosAccesoModels)registroDTO };
            });
        }
    }
}
