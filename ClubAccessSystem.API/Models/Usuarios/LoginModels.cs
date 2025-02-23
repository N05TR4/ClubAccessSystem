using System.ComponentModel.DataAnnotations;

namespace ClubAccessSystem.API.Models.Usuarios
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
