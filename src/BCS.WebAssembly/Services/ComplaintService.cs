using BCS.Infrastructure.Dtos;
using BCS.Infrastructure.Interface;
using System.Net;
using System.Net.Http.Json;

namespace BCS.WebAssembly.Services
{
    public class ComplaintService : IComplaint
    {
        private readonly HttpClient _httpClient;

        public ComplaintService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ComplaintUpdateDto> ItemList { get; set; } = new List<ComplaintUpdateDto>();

        public async Task<List<ComplaintUpdateDto>> GetComplaints()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ComplaintUpdateDto>>("/api/Complaint");
            return result ?? new List<ComplaintUpdateDto>();
        }

        public async Task<ComplaintUpdateDto> GetComplaintById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Complaint/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Unexpected HTTP status code {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<ComplaintUpdateDto>();
        }

        public async Task Create(ComplaintCreateDto model)
        {
            await _httpClient.PostAsJsonAsync("/api/Complaint", model);
        }

        public async Task Edit(Guid id, ComplaintUpdateDto model)
        {
            await _httpClient.PutAsJsonAsync($"api/Complaint/{id}", model);
        }

        public async Task Delete(Guid id)
        {
            await _httpClient.DeleteAsync($"api/Complaint/{id}");
        }
    }
}
