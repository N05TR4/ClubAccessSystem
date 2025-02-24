using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClubAccessSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModels model)
        {
            var token = await _authService.AutenticarAsync(model.Email, model.Password);
            if (token == null) return Unauthorized();

            // Obtén el usuario para devolver la información junto con el token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var usuario = userId != null ? await _authService.VerificarTokenAsync(userId) : null;

            return Ok(new
            {
                token = token,
                user = usuario
            });
        }

        [Authorize] // Este atributo asegura que solo usuarios autenticados puedan acceder
        [HttpGet("verify")]
        public async Task<IActionResult> VerifyToken()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var usuario = await _authService.VerificarTokenAsync(userId);
            if (usuario == null)
                return Unauthorized();

            return Ok(new { user = usuario });
        }
    }
}