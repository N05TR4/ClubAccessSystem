namespace ClubAccessSystem.API.Models.RegistrosAcceso
{
    public abstract class BaseRegistrosAccesoModels
    {
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int ClienteId { get; set; }
    }
}
