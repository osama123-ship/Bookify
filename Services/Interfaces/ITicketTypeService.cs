using Bookify.Models;
using Bookify.ViewModels.TicketTypeVM;

namespace Bookify.Services.Interfaces
{
    public interface ITicketTypeService:IBaseService<TicketType>
    {
        Task AddAsync(NewTicketTypeVM New,int EventId);
        Task<List<TicketType>> GetByEventIdAsync(int EventId);
        Task<List<DetailsForAdminVM>> TicketTypeDetailsForAdmin(int EventId);
    }
}
