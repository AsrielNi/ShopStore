using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;

namespace ShopApplication.Controllers
{
    public class CustomerController : Controller
    {
        public readonly ShopContext _shopContext;
        public CustomerController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Space(string customerName)
        {
            var result = await _shopContext.CustomerInfo.FirstOrDefaultAsync(m => m.CustomerName == customerName);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return View(result);
            }
            
        }
    }
}
