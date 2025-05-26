using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using BussinessLayer.Services.Abstraction;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BussinessLayer.DTOs.Workshop;
using MocViet.ViewModel;
using MocViet.ViewModel.Workshop;

namespace MocViet.Controllers
{
    [Authorize]
    public class WorkshopsController : Controller
    {
        private readonly IWorkshopService _ws;
        private readonly IGenericRepository<Location> _locRepo;

        public WorkshopsController(IWorkshopService ws, IGenericRepository<Location> locRepo)
        {
            _ws = ws;
            _locRepo = locRepo;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var now = DateTime.UtcNow;
            var workshops = await _ws.GetUpcomingOrOngoingAsync();

            var vmList = workshops.Select(w => new ViewModel.Workshop.WorkshopListItemVm
            {
                Id = w.Id,
                Title = w.Title!,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                Fee = w.Fee,
                HostName = w.Host.FullName,
                LocationAddress = w.Location?.Address,
                Status =
                    (w.StartTime.HasValue && w.StartTime > now) ? "Sắp diễn ra"
                  : (w.EndTime.HasValue && w.EndTime < now) ? "Kết thúc"
                  : "Đang diễn ra"
            }).ToList();

            return View(vmList);
        }

        // Hiển thị workshop đã kết thúc — chỉ seller & admin
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Expired()
            => View("Expired", await _ws.GetExpiredAsync());

        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var w = await _ws.GetByIdAsync(id);
            if (w == null) return NotFound();

            var now = DateTime.UtcNow;
            var status = (w.StartTime.HasValue && w.StartTime > now)
                ? "Sắp diễn ra"
                : (w.EndTime.HasValue && w.EndTime < now)
                    ? "Kết thúc"
                    : "Đang diễn ra";

            var vm = new WorkshopDetailsVm
            {
                Id = w.Id,
                Title = w.Title!,
                Description = w.Description,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                MaxParticipants = w.MaxParticipants,
                Fee = w.Fee ?? 0,
                HostName = w.Host.FullName,
                LocationAddress = w.Location?.Address,
                CreatedAt = w.CreatedAt,
                Status = status
            };

            return View(vm);
        }

        // GET: /Workshops/Create
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Locations = (await _locRepo.GetAllAsync())
                .Select(l => new SelectListItem(l.Address ?? "–", l.Id))
                .ToList();
            return View();
        }

        [HttpPost] 
        [Authorize(Roles = "Seller,Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkshopDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = (await _locRepo.GetAllAsync())
                    .Select(l => new SelectListItem(l.Address ?? "–", l.Id))
                    .ToList();
                return View(dto);
            }

            var hostId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            await _ws.CreateAsync(hostId, dto);
            TempData["SuccessMessage"] = "Workshop created!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Workshops/Edit/5
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var w = await _ws.GetByIdAsync(id);
            if (w == null) return NotFound();

            ViewBag.Locations = (await _locRepo.GetAllAsync())
                .Select(l => new SelectListItem(l.Address ?? "–", l.Id, l.Id == w.LocationId))
                .ToList();

            var dto = new UpdateWorkshopDto
            {
                Id = w.Id,
                Title = w.Title!,
                Description = w.Description,
                LocationId = w.LocationId,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                MaxParticipants = w.MaxParticipants,
                Fee = w.Fee
            };
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Seller,Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateWorkshopDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Locations = (await _locRepo.GetAllAsync())
                    .Select(l => new SelectListItem(l.Address ?? "–", l.Id, l.Id == dto.LocationId))
                    .ToList();
                return View(dto);
            }

            await _ws.UpdateAsync(dto.Id, dto);
            TempData["SuccessMessage"] = "Workshop updated!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var w = await _ws.GetByIdAsync(id);
            if (w == null) return NotFound();
            return View(w);
        }

        [HttpPost, ActionName("Delete")] 
        [Authorize(Roles = "Seller,Admin")] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _ws.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
