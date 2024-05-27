using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.Streets
{
    public class StreetRepository(DataContext context) : Repository<Street, Guid>(context), IStreetRepository;
}
