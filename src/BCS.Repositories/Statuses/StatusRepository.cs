using BCS.Core.Context;
using BCS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCS.Repositories.Statuses
{
    public class StatusRepository : Repository<Status, Guid>, IStatusRepository
    {
        public StatusRepository(DataContext context) : base(context)
        {
        }

        public async Task<Guid?> GetIdByTitleAsync(string title)
        {
            var status = await _context.Set<Status>().FirstOrDefaultAsync(s => s.Title == title);
            return status?.Id;
        }
    }
}
