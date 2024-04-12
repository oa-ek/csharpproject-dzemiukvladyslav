using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.Statuses
{
    public class StatusRepository(DataContext context) : Repository<Status, Guid>(context), IStatusRepository;
}
