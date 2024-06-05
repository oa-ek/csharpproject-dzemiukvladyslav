using BCS.Infrastructure.Dtos;

namespace BCS.Infrastructure.Interface
{
    public interface IComplaint
    {
        List<ComplaintUpdateDto> ItemList { get; set; }
        Task<List<ComplaintUpdateDto>> GetComplaints();
        Task<ComplaintUpdateDto> GetComplaintById(Guid id);
        Task Create(ComplaintCreateDto entity);
        Task Edit(Guid id, ComplaintUpdateDto entity);
        Task Delete(Guid id);
    }
}
