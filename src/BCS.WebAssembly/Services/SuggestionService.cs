using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class SuggestionService : ISuggestion
    {
        private readonly HttpClient _httpClient;

        public SuggestionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<SuggestionUpdateDto> ItemList { get; set; } = new List<SuggestionUpdateDto>();

        public async Task GetSuggestions()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SuggestionUpdateDto>>("/api/Suggestion");
            if (result != null)
                ItemList = result;
        }

        public async Task<SuggestionUpdateDto> GetSuggestionById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Suggestion/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<SuggestionUpdateDto>();
        }

        public async Task Create(SuggestionCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Suggestion", model);
        }

        public async Task Edit(Guid id, SuggestionUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Suggestion/{id}", model);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Suggestion/{id}");
        }
    }
}
