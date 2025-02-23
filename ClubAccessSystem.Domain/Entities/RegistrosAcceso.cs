

using ClubAccessSystem.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubAccessSystem.Domain.Entities
{
    [Table("RegistrosAcceso")]
    public class RegistrosAcceso : AuditEntity
    {
        [Key]
        [Column("RegistroId")]
        public int RegistroId { get; set; }
        public DateTime fechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int ClienteId { get; set; }


    }
}
