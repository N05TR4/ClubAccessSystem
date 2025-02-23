namespace ClubAccessSystem.API.Models.Clientes
{
    public abstract class BaseClientesModels
    {
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public int TipoCliente { get; set; }
    }
}
