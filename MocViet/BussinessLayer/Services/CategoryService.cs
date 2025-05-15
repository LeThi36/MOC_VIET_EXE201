using BussinessLayer.DTOs.Category;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        public CategoryService(IGenericRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task CreateAsync(Category category)
        {
            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            await _categoryRepo.CreateAsync(category);
        }

        public async Task DeleteAsync(string id)
        {
            var category = await _categoryRepo.GetAsync(s => s.Id == id);
            if(category != null)
            {
                await _categoryRepo.RemoveAsync(category);
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _categoryRepo.GetAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Category category)
        {
            var existing = await _categoryRepo.GetAsync(s => s.Id == category.Id);
            if(existing != null)
            {
                existing.Name = category.Name;
                existing.Description = category.Description;
                existing.UpdatedAt = DateTime.Now;
                await _categoryRepo.UpdateAsync(existing);
            }
        }
    }
}
