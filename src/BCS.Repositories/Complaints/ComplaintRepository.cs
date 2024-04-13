using BCS.Core.Context;
using BCS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCS.Repositories.Complaints
{
    public class ComplaintRepository(DataContext context)
    : Repository<Complaint, Guid>(context), IComplaintRepository
    {
        public override async Task<IEnumerable<Complaint>> GetAllByUserAsync(AppUser user) =>
        await context.Complaints.Where(x => x.User == user).ToListAsync();
    }
}
