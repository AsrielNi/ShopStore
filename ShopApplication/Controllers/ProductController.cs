using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

namespace ShopApplication.Controllers
{
    public class ProductController : Controller
    {
        public readonly ShopContext _shopContext;
        public ProductController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public async Task<IActionResult> ShowProduct(string productID)
        {
            var result = await _shopContext.ProductData.FirstOrDefaultAsync(m => m.ProductID == productID);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }
        [Route("ProductInfo/{productID}")]
        public async Task<IActionResult> ProductInfo(string productID)
        {
            var result = await _shopContext.ProductData.FirstOrDefaultAsync(m => m.ProductID == productID);
            if (result != null)
            {
                return View(result);
            }
            return NotFound();
        }
    }
}
