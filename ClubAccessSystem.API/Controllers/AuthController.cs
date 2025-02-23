using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.API.Service;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new { Token = token });
        }
    }
}
