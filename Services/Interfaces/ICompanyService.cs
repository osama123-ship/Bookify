using Bookify.Models;
using Bookify.ViewModels.CompanyVM;

namespace Bookify.Services.Interfaces
{
    public interface ICompanyService : IBaseService<Company>
    {
        Task<List<Event>> GetEventsAsync(int id);
        Task<int> GetCompanyIdByUserIdAsync(string UserId);
        Task UpdateCompanyProfileAsync(EditCompanyProfileVM model);
        Task<EditCompanyProfileVM> GetCompanyProfileAsync(string userId);
    }
}
