

namespace ClubAccessSystem.Domain.Base
{
    public abstract class AuditEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
