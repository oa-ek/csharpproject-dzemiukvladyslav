using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("suggestionComments")]
    public class SuggestionComments : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        ////////
        public string UserName { get; set; }

        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public virtual AppUser? User { get; set; }

        [ForeignKey("Suggestion")]
        public Guid SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }

        public DateTime SugCommentData { get; set; }
        public string Text { get; set; }
        public string? SugCommentPhoto { get; set; }
    }
}
