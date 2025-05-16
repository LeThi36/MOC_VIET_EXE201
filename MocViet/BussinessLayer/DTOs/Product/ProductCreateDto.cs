using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussinessLayer.DTOs.Product
{
    public class ProductCreateDto
    {
        public string SellerId { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }

        // Thêm dòng này để tránh lỗi CS1061
        public List<IFormFile> Images { get; set; } = new();

        public List<string> ImageUrls { get; set; } = new();
    }
}
