namespace ClubAccessSystem.API.Models.Clientes
{
    public class UpdateClientesModels : BaseClientesModels
    {

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
