using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface IStreet
    {
        List<StreetUpdateDto> ItemList { get; set; }
        Task GetStreets();
        Task<StreetUpdateDto> GetStreetById(Guid id);
        Task CreateStreet(StreetCreateDto model);
        Task UpdateStreet(Guid id, StreetUpdateDto entity);
        Task DeleteStreet(Guid id);
    }
}
