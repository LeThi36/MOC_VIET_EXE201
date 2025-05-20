using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MocViet.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: /Cart/
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1"; // giả lập nếu chưa login
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }

        // POST: /Cart/Add
        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1";

            await _cartService.AddToCartAsync(userId, productId, 1);

         
            return RedirectToAction("Index", "Product");
        }


        // POST: /Cart/Update
        [HttpPost]
        public async Task<IActionResult> Update(string cartItemId, int quantity)
        {
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);
            return RedirectToAction("Index");
        }

        // POST: /Cart/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string cartItemId)
        {
            await _cartService.RemoveCartItemAsync(cartItemId);
            return RedirectToAction("Index");
        }
    }
}
