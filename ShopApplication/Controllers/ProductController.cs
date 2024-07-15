using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;

namespace ShopApplication.Controllers
{
    public class ProductController : Controller
    {
        public readonly ShopContext _shopContext;
        public ProductController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        // 作為商品清單的Action
        // 顯示部分數量的商品
        public IActionResult Menu()
        {
            var results = _shopContext.ProductData.Take(20);
            if (results != null)
            {
                return View(results);
            }
            return NotFound();
        }

        // 顯示個別商品的資訊
        // 未來考量是否繼續使用路由屬性或是查詢字串
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
