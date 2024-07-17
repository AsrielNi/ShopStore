using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.Controllers
{
    public class ProductController : Controller
    {
        
        // 作為商品清單的Action
        // 顯示部分數量的商品
        public IActionResult Menu()
        {
            return View();
        }

        // 顯示個別商品的資訊
        public async Task<IActionResult> ProductInfo(string productID)
        {
            ViewData["productID"] = productID;
            return View();
        }
    }
}
