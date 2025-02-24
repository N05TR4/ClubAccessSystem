

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
    public class ClientesRepository : BaseRepository<Clientes, ClientesModels>, IClientesRepository
    {
        private readonly ClubContext _dbContext;

        public ClientesRepository(ClubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<List<ClientesTipoClienteModels>>> GetAllClientesTipoCliente()
        {
            return await SafeExecuteAsync(async () =>
            {
                var clientesTipo = await (from c in _dbContext.Clientes
                                        join t in _dbContext.TipoClientes on c.TipoCliente equals t.TipoClienteId
                                        select new ClientesTipoClienteModels
                                        {
                                            ClienteId = c.ClienteId,
                                            Nombre = c.Nombre,
                                            Contacto = c.Contacto,
                                            TipoCliente = c.TipoCliente,
                                            NombreTipoCliente = t.Nombre

                                        }).ToListAsync();

                return new OperationResult<List<ClientesTipoClienteModels>> { Success = true, Data = clientesTipo };
            });
        }

        public async Task<OperationResult<List<ClientesModels>>> GetClientesByTipo(int tipoClienteId)
        {
            if (tipoClienteId <= 0) return new OperationResult<List<ClientesModels>> { Success = false, Message = "ID cannot be null or less than or equal to 0" };

            return await SafeExecuteAsync(async () =>
            {
                var cliente = await _dbContext.Clientes.Where(a => a.TipoCliente == tipoClienteId).ToListAsync();
                var clienteDTO = cliente.Select(a => new ClientesModels
                {
                    ClienteId = a.ClienteId,
                    Nombre = a.Nombre,
                    Contacto = a.Contacto,
                    TipoCliente = a.TipoCliente,
                }).ToList();

                return new OperationResult<List<ClientesModels>>
                {
                    Success = true,
                    Data = clienteDTO // Devuelve la lista completa
                };
            });
        }
    }
}
