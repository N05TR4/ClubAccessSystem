

using ClubAccessSystem.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubAccessSystem.Domain.Entities
{
    [Table("Clientes")]
    public class Clientes : BaseEntity
    {
        [Key]
        [Column("ClienteId")]
        public int ClienteId { get; set; }
        public string Contacto { get; set; }
        public int TipoCliente { get; set; }

    }
}
