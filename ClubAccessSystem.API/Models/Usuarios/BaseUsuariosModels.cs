using System.ComponentModel.DataAnnotations;

namespace ClubAccessSystem.API.Models.Usuarios
{
    public abstract class BaseUsuariosModels
    {
        [Required(ErrorMessage = "Nombre is required")]
        public string Nombre { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public int RolId { get; set; }
    }
}
