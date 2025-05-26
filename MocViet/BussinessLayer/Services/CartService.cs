using BussinessLayer.DTOs.Cart;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Services
{
    public class CartService : ICartService
    {
        private readonly MocVietContext _context;

        public CartService(MocVietContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, string productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsCheckedOut == false);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    IsCheckedOut = false
                };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null) throw new Exception("Product not found");

                var newItem = new CartItem
                {
                    Id = Guid.NewGuid().ToString(),
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price
                };
                _context.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<CartDto?> GetCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsCheckedOut == false);

            if (cart == null) return null;

            return new CartDto
            {
                Id = cart.Id,
                CreatedAt = cart.CreatedAt,
                Items = cart.CartItems.Select(i => new CartItemDto
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

        public async Task UpdateCartItemAsync(string cartItemId, int quantity)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) throw new Exception("Cart item not found");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItemAsync(string cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) throw new Exception("Cart item not found");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
