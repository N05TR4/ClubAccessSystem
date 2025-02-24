

using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Domain.Repositories;
using ClubAccessSystem.Domain.Result;
using ClubAccessSystem.Persistence.Models;

namespace ClubAccessSystem.Persistence.Interfaces
{
    public interface IUsuariosRepository : IBaseRepository<Usuarios, UsuariosModels>
    {
        Task<OperationResult<UsuariosModels>> GetUsuariosByRol(int rolId);
        Task<OperationResult<UsuariosModels>> GetUsuariosByEmail(string email);
        Task<OperationResult<List<UsuariosRolModels>>> GetAllUsuarioRoll();
    }
}
