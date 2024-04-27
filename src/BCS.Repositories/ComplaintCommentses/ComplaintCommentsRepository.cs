using BCS.Core.Context;
using BCS.Core.Entities;

namespace BCS.Repositories.ComplaintCommentses
{
    public class ComplaintCommentsRepository(DataContext context) : Repository<ComplaintComments, Guid>(context), IComplaintCommentsRepository;
}
