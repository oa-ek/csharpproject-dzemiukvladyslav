using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("suggestions")]
    public class Suggestion : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }

        [ForeignKey("Type")]
        public Guid TypeId { get; set; }
        public virtual Type Type { get; set; }

        public string Text { get; set; }
        public DateTime Sdatetime { get; set; }

        [ForeignKey("Status")]
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }

        [ForeignKey("City")]
        public Guid CityId { get; set; }
        public virtual City City { get; set; }

        [ForeignKey("Street")]
        public Guid StreetId { get; set; }
        public virtual Street Street { get; set; }

        [ForeignKey("Structure")]
        public Guid StructureId { get; set; }
        public virtual Structure Structure { get; set; }

        public string? Number { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Photo { get; set; }
        public virtual ICollection<SuggestionComments> SuggestionCommentses { get; set; }

    }
}
