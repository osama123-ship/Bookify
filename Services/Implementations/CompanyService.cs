using Bookify.Extenctions;
using Bookify.Models;
using Bookify.Repositories.Interfaces;
using Bookify.Services.Interfaces;
using Bookify.ViewModels.CompanyVM;
using Microsoft.AspNetCore.Identity;

namespace Bookify.Services.Implementations
{
    public class CompanyService : BaseService<Company>,ICompanyService
    {
        protected readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository, IBaseRepository<Company> repository) : base(repository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<List<Event>> GetEventsAsync(int id)
        {
           return await companyRepository.GetEventsAsync(id);
        }
        public async Task<int> GetCompanyIdByUserIdAsync(string UserId)
        {
            return await companyRepository.GetCompanyIdByUserIdAsync(UserId);
        }
        public async Task<EditCompanyProfileVM> GetCompanyProfileAsync(string userId)
        {
            var company = await companyRepository.GetByUserIdAsync(userId);
            if (company == null)
                throw new KeyNotFoundException("Company not found");

            return new EditCompanyProfileVM
            {
                UserId = company.UserId,
                CompanyName = company.CompanyName,
                Website = company.Website
            };
        }
        public async Task UpdateCompanyProfileAsync(EditCompanyProfileVM model)
        {
            var company = await companyRepository.GetByUserIdAsync(model.UserId);
            if (company == null)
                throw new KeyNotFoundException("Company not found");

            company.CompanyName = model.CompanyName;
            company.Website = model.Website;

            await companyRepository.UpdateAsync(company);
        }

    }

}
