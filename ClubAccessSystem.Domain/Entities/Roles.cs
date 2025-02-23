

using ClubAccessSystem.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubAccessSystem.Domain.Entities
{
    [Table("Roles")]
    public class Roles : BaseEntity
    {
        [Key]
        [Column("RolId")]
        public int RolId { get; set; }
    }
}
