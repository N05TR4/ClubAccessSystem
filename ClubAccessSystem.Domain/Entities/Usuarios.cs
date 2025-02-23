using ClubAccessSystem.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClubAccessSystem.Domain.Entities
{
    [Table("Usuarios")]
    public class Usuarios : BaseEntity
    {
        [Key]
        [Column("UsuarioId")]
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
    }
}
