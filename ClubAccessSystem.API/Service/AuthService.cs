using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClubAccessSystem.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuariosRepository usuariosRepository, IConfiguration configuration)
        {
            _usuariosRepository = usuariosRepository;
            _configuration = configuration;
        }

        public async Task<string?> AutenticarAsync(string email, string password)
        {
            var usuario = await _usuariosRepository.GetUsuariosByEmail(email);
            if (usuario == null || usuario.Data == null)
                return null;

            if (usuario.Data.Password != password)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTSettings:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Data.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Data.Email),
                    new Claim(ClaimTypes.Role, usuario.Data.RolId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JWTSettings:Issuer"],
                Audience = _configuration["JWTSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<UsuarioResponse?> VerificarTokenAsync(string userId)
        {
            var resultado = await _usuariosRepository.GetById(int.Parse(userId));
            if (resultado == null || resultado.Data == null)
                return null;

            // Crear un objeto de respuesta con solo la información necesaria
            return new UsuarioResponse
            {
                UsuarioId = resultado.Data.UsuarioId,
                Email = resultado.Data.Email,
                RolId = resultado.Data.RolId
                // Agrega otros campos necesarios, pero NO incluyas la contraseña
            };
        }
    }

   
}