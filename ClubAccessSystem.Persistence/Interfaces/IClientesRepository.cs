

using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Domain.Repositories;
using ClubAccessSystem.Domain.Result;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Interfaces
{
    public interface IClientesRepository : IBaseRepository<Clientes, ClientesModels>
    {
        Task<OperationResult<List<ClientesModels>>> GetClientesByTipo(int tipoClienteId);
    }
}
