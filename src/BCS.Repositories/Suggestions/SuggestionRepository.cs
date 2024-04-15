using BCS.Core.Context;
using BCS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BCS.Repositories.Suggestions
{
    public class SuggestionRepository(DataContext context)
    : Repository<Suggestion, Guid>(context), ISuggestionRepository
    {
        public override async Task<IEnumerable<Suggestion>> GetAllByUserAsync(AppUser user) =>
        await context.Suggestions.Where(x => x.User == user).ToListAsync();
    }
}
