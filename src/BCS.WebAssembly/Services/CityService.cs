using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class CityService : ICity
    {
        private readonly HttpClient _httpClient;
        public CityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<CityUpdateDto> ItemList { get; set; } = new List<CityUpdateDto>();

        public async Task GetCities()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CityUpdateDto>>("/api/City");
            if (result != null)
                ItemList = result;
        }

        public async Task<CityUpdateDto> GetCityById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/City/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CityUpdateDto>();
        }

        public async Task CreateCity(CityCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/City", model);
        }

        public async Task UpdateCity(Guid id, CityUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/City/{id}", model);
        }

        public async Task DeleteCity(Guid id)
        {
            await _httpClient.DeleteAsync($"api/City/{id}");
        }
    }
}
