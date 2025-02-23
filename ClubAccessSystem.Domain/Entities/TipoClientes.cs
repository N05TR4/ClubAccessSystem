

using ClubAccessSystem.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubAccessSystem.Domain.Entities
{
    [Table("TipoClientes")]
    public class TipoClientes : BaseEntity
    {
        [Key]
        [Column("TipoClienteId")]
        public int TipoClienteId { get; set; }

    }
}
