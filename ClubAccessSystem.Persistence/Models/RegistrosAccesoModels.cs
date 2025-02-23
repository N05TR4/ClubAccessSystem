
namespace ClubAccessSystem.Persistence.Models
{
    public class RegistrosAccesoModels
    {
        public int RegistroId { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int ClienteId { get; set; }
    }
}
