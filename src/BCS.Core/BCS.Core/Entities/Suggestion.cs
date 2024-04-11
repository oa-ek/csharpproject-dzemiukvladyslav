using System.ComponentModel.DataAnnotations.Schema;

namespace BCS.Core.Entities
{
    [Table("suggestions")]
    public class Suggestion : IBaseEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid TypeId { get; set; }
        public virtual Type Type { get; set; }
        public string Text { get; set; }
        public DateTime Sdatetime { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public Guid StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string Number { get; set; }
        public string Photo { get; set; }
    }
}
