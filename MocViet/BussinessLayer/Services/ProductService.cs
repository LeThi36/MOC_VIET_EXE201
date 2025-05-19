using BussinessLayer.DTOs.Product;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductImage> _imageRepo;
        private readonly  MocVietContext _context;




        public ProductService(IGenericRepository<Product> productRepo,
                              IGenericRepository<ProductImage> imageRepo,
              MocVietContext context)
        {
            _productRepo = productRepo;
            _imageRepo = imageRepo;
            _context = context;
        }

        public async Task CreateAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                SellerId = dto.SellerId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _productRepo.CreateAsync(product);

            foreach (var imageUrl in dto.ImageUrls)
            {
                var image = new ProductImage
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = product.Id,
                    ImageUrl = imageUrl
                };
                await _imageRepo.CreateAsync(image);
            }
        }

        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var product = await _productRepo.GetAsync(p => p.Id == dto.Id);
            if (product == null) throw new Exception("Product not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.IsActive = dto.IsActive;
            product.UpdatedAt = DateTime.Now;

            await _productRepo.UpdateAsync(product);

            // Xóa ảnh
            foreach (var imgId in dto.DeleteImageIds)
            {
                var img = await _imageRepo.GetAsync(i => i.Id == imgId);
                if (img != null)
                {
                    await _imageRepo.RemoveAsync(img);
                    // Việc xóa file vật lý trên ổ đĩa sẽ xử lý trong Controller
                }
            }

            // Thêm ảnh mới
            foreach (var imageUrl in dto.NewImageUrls)
            {
                var image = new ProductImage
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = product.Id,
                    ImageUrl = imageUrl
                };

                await _imageRepo.CreateAsync(image);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _productRepo.GetAsync(p => p.Id == id);
            if (product == null) return;

            var images = await _imageRepo.GetAllAsync(i => i.ProductId == id);
            foreach (var img in images)
            {
                await _imageRepo.RemoveAsync(img);
                // Việc xóa file vật lý sẽ xử lý ở controller nếu cần
            }

            await _productRepo.RemoveAsync(product);
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                IsActive = p.IsActive,
                SellerId = p.SellerId,
                CategoryId = p.CategoryId,
                ProductImages = p.ProductImages.Select(i => new ProductImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    ProductId = i.ProductId
                }).ToList()
            });
        }



    }
}
