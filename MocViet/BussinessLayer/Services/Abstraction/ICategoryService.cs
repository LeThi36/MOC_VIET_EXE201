using BussinessLayer.DTOs.Category;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);
        Task<Category> GetByIdAsync(string id);
    }
}
