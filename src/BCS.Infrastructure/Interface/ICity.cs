using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface ICity
    {
        List<CityUpdateDto> ItemList { get; set; }
        Task GetCities();
        Task<CityUpdateDto> GetCityById(Guid id);
        Task CreateCity(CityCreateDto model);
        Task UpdateCity(Guid id, CityUpdateDto entity);
        Task DeleteCity(Guid id);
    }
}
