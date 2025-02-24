using AutoMapper;
using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Context;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubAccessSystem.API.Controllers
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ClubContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuariosRepository usuariosRepository, ClubContext context, IMapper mapper, ILogger<UsuarioController> logger)
        {
            _usuariosRepository = usuariosRepository;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("getAllUsuario")]
        public async Task<IActionResult> GetAllUsuario()
        {
            _logger.LogInformation("Getting all users");
            var usuarios = await _usuariosRepository.GetAllUsuarioRoll();

            if (!usuarios.Success)
            {
                _logger.LogWarning("No users found");
                return NotFound(new { message = "No hay usuarios disponibles." });
            }

            _logger.LogInformation("Successfully retrieved {Count} users", usuarios.Data?.Count ?? 0);
            return Ok(usuarios);
        }

        [HttpGet("getUsuarioById/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            _logger.LogInformation("Getting user with ID: {UserId}", id);

            var usuario = await _usuariosRepository.GetById(id);
            if (!usuario.Success || usuario.Data == null)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return NotFound(new { message = $"El usuario con el ID {id} no fue encontrado." });
            }

            _logger.LogInformation("Successfully retrieved user with ID: {UserId}", id);
            return Ok(usuario);
        }


        [HttpPost("createUsuario")]
        public async Task<IActionResult> CreateUsuario([FromBody] AddUsuariosModels usuario)
        {
            _logger.LogInformation("Creating new user");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for user creation");
                return BadRequest(ModelState);
            }

            var usuarios = _mapper.Map<Usuarios>(usuario);
            var result = await _usuariosRepository.Save(usuarios);

            if (!result.Success)
            {
                _logger.LogError("Failed to create user: {Message}", result.Message);
                return BadRequest(new { message = result.Message });
            }

            _logger.LogInformation("Successfully created new user");
            return Ok(new { message = "Operación Exitosa!" });
        }


        [HttpPut("updateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuariosModels usuario)
        {
            _logger.LogInformation("Updating user with ID: {UserId}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for user update");
                return BadRequest(ModelState);
            }

            var result = await _usuariosRepository.GetById(id);
            if (!result.Success || result.Data == null)
            {
                _logger.LogWarning("User with ID {UserId} not found for update", id);
                return NotFound(new { message = $"El usuario con el ID: {id} no fue encontrado." });
            }

            var existingUser = _mapper.Map<Usuarios>(result.Data);
            _mapper.Map(usuario, existingUser);
            existingUser.UpdatedAt = DateTime.Now;

            var updateResult = await _usuariosRepository.Update(existingUser);
            if (!updateResult.Success)
            {
                _logger.LogError("Failed to update user: {Message}", updateResult.Message);
                return BadRequest(new { message = updateResult.Message });
            }

            _logger.LogInformation("Successfully updated user with ID: {UserId}", id);
            return Ok(new { message = "Operación Exitosa!" });
        }

        [HttpDelete("deleteUsuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                
                var usuario = await _context.Usuarios.FindAsync(id);

                if (usuario == null)
                {
                    return NotFound(new { Success = false, Message = "Usuario no encontrado" });
                }

                // Llamar al método Delete del repositorio
                var result = await _usuariosRepository.Delete(usuario);

                if (result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = $"Error al eliminar el usuario: {ex.Message}"
                });
            }
        }

    }
}
