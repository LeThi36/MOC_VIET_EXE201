using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Stock { get; set; }
        public bool IsActive { get; set; }

        public List<string> NewImageUrls { get; set; } = new();
        public List<string> DeleteImageIds { get; set; } = new();
    }

}
