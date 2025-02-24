using ClubAccessSystem.API.Models.Usuarios;

namespace ClubAccessSystem.API.Service
{
    public interface IAuthService
    {
        Task<string?> AutenticarAsync(string email, string password);
        Task<UsuarioResponse> VerificarTokenAsync(string userId);
    }
}
