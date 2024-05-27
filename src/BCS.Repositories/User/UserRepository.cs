using BCS.Core.Context;
using BCS.Core.Entities;
using BCS.Repositories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BCS.Repositories.User
{
    public class UserRepository : Repository<AppUser, Guid>, IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserRepository(DataContext _ctx,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager) : base(_ctx)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<AppUser> CreateWithPasswordAsync(UserCreateModel model)
        {
            var newUser = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = model.Email,
                FullName = model.Name,
                EmailConfirmed = false,
                NormalizedUserName = model.Email.ToUpper(),
                NormalizedEmail = model.Email.ToUpper(),
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            return await _context.Users.FirstAsync(x => x.Email == model.Email);
        }
        public async Task<IEnumerable<UserListItemModel>> GetAllWithRolesAsync()
        {
            var list = new List<UserListItemModel>();

            foreach (var user in await _context.Users.ToListAsync())
            {
                var userModel = new UserListItemModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Roles = new List<IdentityRole<Guid>>()
                };

                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    userModel.Roles.Add(await _context.Roles.FirstAsync(x => x.Name.ToLower() == role.ToLower()));
                }

                list.Add(userModel);
            }

            return list;
        }

        public async Task UpdateUserAsync(UserListItemModel model, string[] roles)
        {
            var user = _context.Users.Find(model.Id);

            if (user.Email != model.Email)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToUpper();
                user.NormalizedEmail = model.Email.ToUpper();
            }

            if (user.FullName != model.FullName)
                user.FullName = model.FullName;

            if (user.EmailConfirmed != model.IsEmailConfirmed)
                user.EmailConfirmed = model.IsEmailConfirmed;

            if ((await _userManager.GetRolesAsync(user)).Any())
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            }

            if (roles.Any())
            {
                await _userManager.AddToRolesAsync(user, roles.ToList());
            }
        }


        public async Task<UserListItemModel> GetOneWithRolesAsync(Guid id)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);

            var userModel = new UserListItemModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                IsEmailConfirmed = user.EmailConfirmed,
                Roles = new List<IdentityRole<Guid>>()
            };

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                userModel.Roles.Add(await _context.Roles.FirstAsync(x => x.Name.ToLower() == role.ToLower()));
            }

            return userModel;
        }

        public async Task<IEnumerable<IdentityRole<Guid>>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<bool> CheckUser(Guid id)
        {
            var user = _context.Users.Find(id);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.All(x => x != "Admin");
        }

        public async Task DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);

            if (await CheckUser(id))
            {
                if ((await _userManager.GetRolesAsync(user)).Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                }

                await _userManager.DeleteAsync(user);
            }
        }
    }
}
