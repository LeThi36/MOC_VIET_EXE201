using BussinessLayer.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services.Abstraction
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, string productId, int quantity);
        Task<CartDto?> GetCartAsync(string userId);
        Task UpdateCartItemAsync(string cartItemId, int quantity);
        Task RemoveCartItemAsync(string cartItemId);
    }
}
