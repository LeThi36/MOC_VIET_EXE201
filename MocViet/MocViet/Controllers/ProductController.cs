using BussinessLayer.DTOs.Product;
using BussinessLayer.Services.Abstraction;
using DataLayer.Entities;
using DataLayer.Enum;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace MocViet.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        private readonly IGenericRepository<User> _sellerRepo;

        public ProductController(
     IProductService productService,
     IWebHostEnvironment env,
     ICategoryService categoryService,
     IGenericRepository<User> sellerRepo)
        {
            _productService = productService;
            _env = env;
            _categoryService = categoryService;
            _sellerRepo = sellerRepo;
        }


        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            ViewBag.Sellers = (await _sellerRepo.GetAllAsync())
    .Where(u => u.UserRole == Role.Seller)
    .ToList();

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto, List<IFormFile> images)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllAsync();
                ViewBag.Sellers = await _sellerRepo.GetAllAsync();
                return View(dto);
            }

            dto.ImageUrls = new List<string>();

            foreach (var file in images)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var path = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                dto.ImageUrls.Add("/uploads/" + fileName);
            }

            await _productService.CreateAsync(dto);
            return RedirectToAction("Index");
        }


        // GET: Product/Update/{id}
        public async Task<IActionResult> Update(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var dto = new ProductUpdateDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                IsActive = product.IsActive ?? true
        };

            ViewBag.ExistingImages = product.ProductImages;

            return View(dto);
        }

        // POST: Product/Update
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto dto, List<IFormFile> newImages)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Lấy danh sách ID ảnh cần xóa từ checkbox
            var deleteImageIds = Request.Form["DeleteImageIds"];
            dto.DeleteImageIds = deleteImageIds.ToList();

            // Upload ảnh mới
            dto.NewImageUrls = new List<string>();
            foreach (var file in newImages)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var path = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                dto.NewImageUrls.Add("/uploads/" + fileName);
            }

            // Xóa file vật lý ảnh nếu có
            if (dto.DeleteImageIds != null && dto.DeleteImageIds.Any())
            {
                var product = await _productService.GetByIdAsync(dto.Id);
                foreach (var img in product.ProductImages)
                {
                    if (dto.DeleteImageIds.Contains(img.Id))
                    {
                        var fullPath = Path.Combine(_env.WebRootPath, img.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                    }
                }
            }

            await _productService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }


        // POST: Product/Delete/{id}
        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            foreach (var img in product.ProductImages)
            {
                var fullPath = Path.Combine(_env.WebRootPath, img.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }

            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Index
        public async Task<IActionResult> Index()
        {
            var productDtos = await _productService.GetAllAsync(); 
            return View(productDtos); 
        }

        // GET: Product/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
