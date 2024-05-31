using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface IType
    {
        List<TypeUpdateDto> ItemList { get; set; }
        Task GetTypes();
        Task<TypeUpdateDto> GetTypeById(Guid id);
        Task CreateType(TypeCreateDto model);
        Task UpdateType(Guid id, TypeUpdateDto entity);
        Task DeleteType(Guid id);
    }
}
