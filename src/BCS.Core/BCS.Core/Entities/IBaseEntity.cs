using System.ComponentModel.DataAnnotations;

namespace BCS.Core.Entities
{
    public interface IBaseEntity<T>
    {
        [Key]
        T Id { get; set; }
    }
}
