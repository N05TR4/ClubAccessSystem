namespace ClubAccessSystem.API.Models.TipoClientes
{
    public class UpdateTipoClientesModels : BaseTipoClientesModels
    {
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
