

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
    public class UsuariosRepository : BaseRepository<Usuarios, UsuariosModels>, IUsuariosRepository
    {
        private readonly ClubContext _dbContext;

        public UsuariosRepository(ClubContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _dbContext = dbContext;

        }



        public async Task<OperationResult<UsuariosModels>> GetUsuariosByEmail(string email)
        {
            if (email is null) return new OperationResult<UsuariosModels> { Success = false, Message = "Email cannot be null" };

            return await SafeExecuteAsync(async () =>
            {
                var usuario = await _dbContext.Usuarios
                    .Where(a => a.Email == email)
                    .Select(a => new UsuariosModels
                    {
                        UsuarioId = a.UsuarioId,
                        Nombre = a.Nombre,
                        Email = a.Email,
                        Password = a.Password,
                        RolId = a.RolId,
                    })
                    .FirstOrDefaultAsync();

                return new OperationResult<UsuariosModels> { Success = usuario != null, Data = usuario, Message = usuario == null ? "Usuario no encontrado" : null };
            });
        }

        public async Task<OperationResult<UsuariosModels>> GetUsuariosByRol(int rolId)
        {
            if (rolId <= 0) return new OperationResult<UsuariosModels> { Success = false, Message = "ID cannot be null or less than or equal to 0" };

            return await SafeExecuteAsync(async () =>
            {
                var usuario = await _dbContext.Usuarios.Where(a => a.RolId == rolId).ToListAsync();
                var usuarioDTO = usuario.Select(a => new UsuariosModels
                {
                    UsuarioId = a.UsuarioId,
                    Nombre = a.Nombre,
                    Email = a.Email,
                    Password = a.Password,
                    RolId = a.RolId,
                });

                return new OperationResult<UsuariosModels> { Success = true, Data = (UsuariosModels)usuarioDTO };
            });
        }
    }
}
