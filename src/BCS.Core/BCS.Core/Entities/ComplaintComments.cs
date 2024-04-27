using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("complaintComments")]
    public class ComplaintComments : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }


        [ForeignKey("Complaint")]
        public Guid ComplaintId { get; set; }
        public virtual Complaint Complaint { get; set; }

        public DateTime ComCommentData { get; set; }
        public string Text { get; set; }
        public string? ComCommentPhoto { get; set; }
    }
}
