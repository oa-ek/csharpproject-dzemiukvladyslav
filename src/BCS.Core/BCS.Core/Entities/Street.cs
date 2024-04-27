using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("street")]
    public class Street : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
    }
}
