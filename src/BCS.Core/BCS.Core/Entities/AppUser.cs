using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("users")]
    public class AppUser : IdentityUser<Guid>, IBaseEntity<Guid>
    {
        public string FullName { get; set; }
        public virtual ICollection<Complaint> Complaints { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
        public virtual ICollection<SuggestionComments> SuggestionCommentses { get; set; }
        public virtual ICollection<ComplaintComments> ComplaintCommentses { get; set; }

    }
}
