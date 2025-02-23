namespace ClubAccessSystem.API.Models.Roles
{
    public class UpdateTipoClientesModels : BaseTipoClientesModels
    {
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
