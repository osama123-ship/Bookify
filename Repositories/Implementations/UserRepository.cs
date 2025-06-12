using Bookify.Data;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories.Implementations
{
    [Authorize(Roles ="Admin")]
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(Context context, UserManager<ApplicationUser> userManager) : base(context)
        {
            this.userManager = userManager;
        }

        public async Task<List<UserVM>> GetUsersAsync(string? search, string? role)
        {
            var users = await userManager.Users.ToListAsync();
            var result = new List<UserVM>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                bool roleFilter = !string.IsNullOrEmpty(role) && role != "None" && !roles.Contains(role);
                bool searchFilter = !string.IsNullOrEmpty(search) &&
                    !user.UserName.Contains(search, StringComparison.OrdinalIgnoreCase) &&
                    !user.Email.Contains(search, StringComparison.OrdinalIgnoreCase);

                if (roleFilter || searchFilter)
                    continue;

                result.Add(new UserVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Role = roles.FirstOrDefault() ?? "None"
                });
            }

            return result;
        }


        public async Task<List<string>> GetRolesAsync()
        {
            return await context.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<ApplicationUser?> GetByIdAsync(string id)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task UpdateUserAsync(ApplicationUser user, string role)
        {
            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);
            await userManager.AddToRoleAsync(user, role);

            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
        public Context Context => context;
    }
}

