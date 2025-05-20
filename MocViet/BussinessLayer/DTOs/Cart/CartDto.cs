using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DTOs.Cart
{
    public class CartDto
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal Total => Items.Sum(item => item.Total);
    }
}
