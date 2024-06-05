using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface IStatus
    {
        List<StatusUpdateDto> ItemList { get; set; }
        Task GetStatuses();
        Task<StatusUpdateDto> GetStatusById(Guid id);
        Task CreateStatus(StatusCreateDto model);
        Task UpdateStatus(Guid id, StatusUpdateDto entity);
        Task DeleteStatus(Guid id);
    }
}
