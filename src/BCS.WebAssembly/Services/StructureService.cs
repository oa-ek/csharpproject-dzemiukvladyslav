using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class StructureService : IStructure
    {
        private readonly HttpClient _httpClient;
        public StructureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<StructureUpdateDto> ItemList { get; set; } = new List<StructureUpdateDto>();

        public async Task GetStructures()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StructureUpdateDto>>("/api/Structure");
            if (result != null)
                ItemList = result;
        }

        public async Task<StructureUpdateDto> GetStructureById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Structure/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<StructureUpdateDto>();
        }

        public async Task CreateStructure(StructureCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Structure", model);
        }

        public async Task UpdateStructure(Guid id, StructureUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Structure/{id}", model);
        }

        public async Task DeleteStructure(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Structure/{id}");
        }
    }
}
