namespace ClubAccessSystem.API.Service
{
    public interface IAuthService
    {
        Task<string?> AutenticarAsync(string email, string password);
    }
}
