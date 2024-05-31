using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class StatusService : IStatus
    {
        private readonly HttpClient _httpClient;
        public StatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<StatusUpdateDto> ItemList { get; set; } = new List<StatusUpdateDto>();

        public async Task GetStatuses()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StatusUpdateDto>>("/api/Status");
            if (result != null)
                ItemList = result;
        }

        public async Task<StatusUpdateDto> GetStatusById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Status/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<StatusUpdateDto>();
        }

        public async Task CreateStatus(StatusCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Status", model);
        }

        public async Task UpdateStatus(Guid id, StatusUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Status/{id}", model);
        }

        public async Task DeleteStatus(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Status/{id}");
        }
    }
}
