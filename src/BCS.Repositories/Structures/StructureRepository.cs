using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.Structures
{
    public class StructureRepository(DataContext context) : Repository<Structure, Guid>(context), IStructureRepository;
}
