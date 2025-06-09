using Bookify.Models;
using Bookify.ViewModels.UserVM;

namespace Bookify.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<List<UserVM>> GetUsersAsync(string? search, string? role);
        Task<List<string>> GetRolesAsync();
        Task<ApplicationUser?> GetByIdAsync(string id);
        Task UpdateUserAsync(ApplicationUser user, string role);

        Task DeleteByIdAsync(string id);
    }
}
