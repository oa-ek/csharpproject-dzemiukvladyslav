using BCS.Core.Entities;

namespace BCS.Repositories.Statuses
{
    public interface IStatusRepository : IRepository<Status, Guid>
    {
        Task<Guid?> GetIdByTitleAsync(string title);
    }
}
