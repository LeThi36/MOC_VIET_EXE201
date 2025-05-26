using BussinessLayer.DTOs.Product;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface IProductService
    {
        Task CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(ProductUpdateDto dto);
        Task DeleteAsync(string id);
        Task<Product> GetByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllAsync();


    }
}
