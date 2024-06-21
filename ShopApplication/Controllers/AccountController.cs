using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;

namespace ShopApplication.Controllers
{
    // 處理消費者資料用的控制器
    public class AccountController : Controller
    {
        // 儲存商家資料的資料庫
        public readonly ShopContext _shopContext;
        
        // 控制器的初始化建構式
        public AccountController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        
        // 預設的首頁，目前沒有特別的規劃
        public IActionResult Index()
        {
            return View();
        }
        
        // 消費者的個人空間，預計配合登入系統做調整
        // 保持登入的方式會採取 'Cookies - Session'
        [HttpPost]
        public async Task<IActionResult> Space(string customerName)
        {
            var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(m => m.AccountName == customerName);

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
