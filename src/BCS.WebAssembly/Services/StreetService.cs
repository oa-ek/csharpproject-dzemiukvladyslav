using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class StreetService : IStreet
    {
        private readonly HttpClient _httpClient;
        public StreetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<StreetUpdateDto> ItemList { get; set; } = new List<StreetUpdateDto>();

        public async Task GetStreets()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StreetUpdateDto>>("/api/Street");
            if (result != null)
                ItemList = result;
        }

        public async Task<StreetUpdateDto> GetStreetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Street/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<StreetUpdateDto>();
        }

        public async Task CreateStreet(StreetCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Street", model);
        }

        public async Task UpdateStreet(Guid id, StreetUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Street/{id}", model);
        }

        public async Task DeleteStreet(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Street/{id}");
        }
    }
}
