using Bookify.ViewModels.RolesVM;
using Microsoft.AspNetCore.Identity;

namespace Bookify.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllAsync();
        Task<IdentityResult> AddAsync(NewRoleVM newRole);
        Task<bool> AddRoleToUserAsync(RoleToUserVM roleToUser);
        Task CheckIfCompany(RoleToUserVM roleToUser);
        Task<IdentityRole?> GetByIdAsync(string id);
        Task<IdentityResult> UpdateAsync(EditRoleVM roleVM);
        Task<IdentityResult> DeleteAsync(string id);
    }
}
