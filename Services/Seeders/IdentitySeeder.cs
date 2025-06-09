
using Microsoft.AspNetCore.Identity;
namespace Bookify.Services.Seeders
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
         IConfiguration _config)
        {
            var roles = new[] { "Admin", "Company", "User" };

            // إنشاء الرولز
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = _config["EmailSettings:From"];
            var adminPassword = _config["EmailSettings:AdminPassword"];

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    // optional: ترصد الأخطاء باللوج أو ترمي استثناء
                    throw new Exception("Failed to create default admin user");
                }
            }
        }
    }
}


