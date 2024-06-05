using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface ISuggestion
    {
        List<SuggestionUpdateDto> ItemList { get; set; }
        Task GetSuggestions();
        Task<SuggestionUpdateDto> GetSuggestionById(Guid id);
        Task Create(SuggestionCreateDto entity);
        Task Edit(Guid id, SuggestionUpdateDto entity);
        Task Delete(Guid id);
    }
}
