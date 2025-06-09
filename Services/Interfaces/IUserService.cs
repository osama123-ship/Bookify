using Bookify.ViewModels.UserVM;

namespace Bookify.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserVM>> GetUsersAsync(string? search, string? role);
        Task<List<string>> GetRolesAsync();
        Task<UserVM?> GetUserByIdAsync(string id);
        Task DeleteUserAsync(string id);
        Task UpdateUserAsync(UserVM user);

    }
}
