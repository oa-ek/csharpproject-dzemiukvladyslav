using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface IStructure
    {
        List<StructureUpdateDto> ItemList { get; set; }
        Task GetStructures();
        Task<StructureUpdateDto> GetStructureById(Guid id);
        Task CreateStructure(StructureCreateDto model);
        Task UpdateStructure(Guid id, StructureUpdateDto entity);
        Task DeleteStructure(Guid id);
    }
}
