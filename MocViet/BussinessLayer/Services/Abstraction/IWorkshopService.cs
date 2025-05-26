using BussinessLayer.DTOs.Workshop;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface IWorkshopService
    {
        Task<IEnumerable<Workshop>> GetUpcomingOrOngoingAsync();
        Task<IEnumerable<Workshop>> GetExpiredAsync();
        Task<Workshop?> GetByIdAsync(string id);
        Task CreateAsync(string hostId, CreateWorkshopDto dto);
        Task UpdateAsync(string id, UpdateWorkshopDto dto);
        Task DeleteAsync(string id);
    }

}
