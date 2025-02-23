

using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Domain.Repositories;
using ClubAccessSystem.Domain.Result;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Interfaces
{
    public interface IRegistrosAccesoRepository : IBaseRepository<RegistrosAcceso, RegistrosAccesoModels>
    {
        Task<OperationResult<RegistrosAccesoModels>> GetClienteById(int clienteId);
    }
}
