using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Category
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
