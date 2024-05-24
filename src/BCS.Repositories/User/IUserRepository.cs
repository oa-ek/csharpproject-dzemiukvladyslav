using BCS.Core.Entities;
using BCS.Repositories.Models;
using Microsoft.AspNetCore.Identity;

namespace BCS.Repositories.User
{
    public interface IUserRepository : IRepository<AppUser, Guid>
    {
        Task<IEnumerable<UserListItemModel>> GetAllWithRolesAsync();
        Task<AppUser> CreateWithPasswordAsync(UserCreateModel model);
        Task<IEnumerable<IdentityRole<Guid>>> GetRolesAsync();
        Task<UserListItemModel> GetOneWithRolesAsync(Guid id);
        Task UpdateUserAsync(UserListItemModel model, string[] roles);

        Task<bool> CheckUser(Guid id);
        Task DeleteUser(Guid id);
    }
}
