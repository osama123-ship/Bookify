using Bookify.Models;
using Bookify.Repositories.Implementations;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<List<UserVM>> GetUsersAsync(string? search, string? role)
        {
            return await userRepository.GetUsersAsync(search, role);
        }

        public async Task<List<string>> GetRolesAsync()
        {
            return await userRepository.GetRolesAsync();
        }

        public async Task<UserVM?> GetUserByIdAsync(string id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return null;

            var roles = await userManager.GetRolesAsync(user);
            return new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Role = roles.FirstOrDefault() ?? "None"
            };
        }
        public async Task UpdateUserAsync(UserVM userVm)
        {
            var user = await userRepository.GetByIdAsync(userVm.Id);
            if (user == null) return;

            user.Email = userVm.Email;
            user.UserName = userVm.UserName;

            await userRepository.UpdateUserAsync(user, userVm.Role);
        }
        public async Task DeleteUserAsync(string id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return;

            var context = ((UserRepository)userRepository).Context;

            var userTickets = await context.Tickets
                .Include(t => t.TicketType)
                .ThenInclude(tt => tt.Event)
                .Where(t => t.UserId == id)
                .ToListAsync();

            foreach (var ticket in userTickets)
            {
                // زودلي التذاكر المتاحة 
                ticket.TicketType.ConfirmedTickets = Math.Max(0, ticket.TicketType.ConfirmedTickets - 1);

               // السيلز بتاعت الايفنت الخاص بالتذكره دي
                ticket.TicketType.Event.Sales -= ticket.TicketType.Price;

               // حذف التذكره من اليوزر 
                context.Tickets.Remove(ticket);
            }

            await context.SaveChangesAsync();

            // حذف اليوزر نفسه
            await userRepository.DeleteByIdAsync(id);
        }

    }
}
