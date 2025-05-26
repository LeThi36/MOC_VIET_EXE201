using BussinessLayer.DTOs.Workshop;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IGenericRepository<Workshop> _wsRepo;
        public WorkshopService(IGenericRepository<Workshop> wsRepo)
        {
            _wsRepo = wsRepo;
        }
        public async Task<IEnumerable<Workshop>> GetUpcomingOrOngoingAsync()
        {
            var now = DateTime.UtcNow;
            // include Host và Location
            var all = await _wsRepo.GetAllAsync(
                filter: null,
                includes: q => q
                    .Include(w => w.Host)
                    .Include(w => w.Location)
            );

            return all
                .Where(w => !w.EndTime.HasValue || w.EndTime >= now)
                .OrderBy(w => w.StartTime);
        }

        public async Task<IEnumerable<Workshop>> GetExpiredAsync()
        {
            var now = DateTime.UtcNow;
            return (await _wsRepo.GetAllAsync(
                       includes: q => q.Include(w => w.Host).Include(w => w.Location)))
                   .Where(w => w.EndTime.HasValue && w.EndTime < now)
                   .OrderByDescending(w => w.EndTime);
        }

        public async Task<Workshop?> GetByIdAsync(string id)
        {
            // Giả sử _wsRepo có GetAllAsync cho includes:
            var list = await _wsRepo.GetAllAsync(
                w => w.Id == id,
                q => q.Include(x => x.Host).Include(x => x.Location)
            );
            return list.FirstOrDefault();
        }

        public async Task CreateAsync(string hostId, CreateWorkshopDto dto)
        {
            var w = new Workshop
            {
                HostId = hostId,
                Title = dto.Title,
                Description = dto.Description,
                LocationId = dto.LocationId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                MaxParticipants = dto.MaxParticipants,
                Fee = dto.Fee,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            SetActiveFlag(w);
            await _wsRepo.CreateAsync(w);
        }

        public async Task UpdateAsync(string id, UpdateWorkshopDto dto)
        {
            var w = await _wsRepo.GetAsync(x => x.Id == id);
            if (w == null) throw new KeyNotFoundException();
            w.Title = dto.Title;
            w.Description = dto.Description;
            w.LocationId = dto.LocationId;
            w.StartTime = dto.StartTime;
            w.EndTime = dto.EndTime;
            w.MaxParticipants = dto.MaxParticipants;
            w.Fee = dto.Fee;
            SetActiveFlag(w);
            w.UpdatedAt = DateTime.Now;
            await _wsRepo.UpdateAsync(w);
        }

        public async Task DeleteAsync(string id)
        {
            var w = await _wsRepo.GetAsync(x => x.Id == id);
            if (w != null)
                await _wsRepo.RemoveAsync(w);
        }

        private void SetActiveFlag(Workshop w)
        {
            var now = DateTime.Now;
            if (w.StartTime.HasValue && w.StartTime > now ||
                w.EndTime.HasValue && w.EndTime < now)
                w.IsActive = false;
            else
                w.IsActive = true;
        }
    }
}
