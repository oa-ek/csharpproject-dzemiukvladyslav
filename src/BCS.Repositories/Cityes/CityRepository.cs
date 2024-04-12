using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.Cityes
{
    public class CityRepository(DataContext context) : Repository<City, Guid>(context), ICityRepository;
}
