using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.RolesVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Bookify.Services.Implementations
{
    public class RoleService:BaseService<IdentityRole>, IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            IBaseRepository<IdentityRole> repository) : base(repository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<List<IdentityRole>> GetAllAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }
        public async Task<IdentityResult> AddAsync(NewRoleVM newRole)
        {
            IdentityRole role = new IdentityRole();
            role.Name = newRole.Name;
            return await roleManager.CreateAsync(role);
        }
        public async Task<bool> AddRoleToUserAsync(RoleToUserVM roleToUser)
        {
            ApplicationUser user = await userManager.FindByNameAsync(roleToUser.UserName);
            if (user == null)
                return false;
            await userManager.AddToRoleAsync(user, roleToUser.RoleName);
            return true;
        }
        public async Task CheckIfCompany(RoleToUserVM roleToUser)
        {
            StringBuilder Name = new StringBuilder(roleToUser.RoleName);
            string Check = "COMPANY";
            for (int i = 0; i < Name.Length; i++)
                if (Name[i] > 'Z')
                    Name[i] = (char)(Name[i] - 32);

            bool ok = Check.Length == Name.Length;
            for (int i = 0; i < Name.Length && ok; i++)
                ok &= Name[i] == Check[i];

            if (!ok)
                return;
            ApplicationUser user = await userManager.FindByNameAsync(roleToUser.UserName);
            Company company = new Company { UserId = user.Id };
            await repository.AddCompanyAsync(company);
        }



        public async Task<IdentityRole?> GetByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> UpdateAsync(EditRoleVM roleVM)
        {
            var role = await roleManager.FindByIdAsync(roleVM.Id);
            if (role == null) return IdentityResult.Failed();

            role.Name = roleVM.Name;
            return await roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return IdentityResult.Failed();

            return await roleManager.DeleteAsync(role);
        }
    }
}
