using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.SuggestionCommentses
{
    public class SuggestionCommentsRepository(DataContext context) : Repository<SuggestionComments, Guid>(context), ISuggestionCommentsRepository;
}
