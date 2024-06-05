using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class TypeService : IType
    {
        private readonly HttpClient _httpClient;
        public TypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<TypeUpdateDto> ItemList { get; set; } = new List<TypeUpdateDto>();

        public async Task GetTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TypeUpdateDto>>("/api/Type");
            if (result != null)
                ItemList = result;
        }

        public async Task<TypeUpdateDto> GetTypeById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Type/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<TypeUpdateDto>();
        }

        public async Task CreateType(TypeCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Type", model);
        }

        public async Task UpdateType(Guid id, TypeUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Type/{id}", model);
        }

        public async Task DeleteType(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Type/{id}");
        }
    }
}
