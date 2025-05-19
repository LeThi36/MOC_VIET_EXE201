using System.Collections.Generic;

namespace BussinessLayer.DTOs.Product
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string SellerId { get; set; }
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public bool? IsActive { get; set; }
        public string? SellerName { get; set; }
        public string? CategoryName { get; set; }
        public List<ProductImageDto> ProductImages { get; set; } = new();

        public List<string> ImageUrls => ProductImages?.ConvertAll(i => i.ImageUrl) ?? new();
    }
}
