using BCS.Core.Context;

namespace BCS.Repositories.Types
{
    public class TypeRepository(DataContext context) : Repository<Core.Entities.Type, Guid>(context), ITypeRepository;
}
