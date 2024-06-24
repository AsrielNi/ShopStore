using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

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
        public async Task<IActionResult> Index()
        {
            HttpRequest request = HttpContext.Request;
            string accountSession = request.Cookies["AccountSession"];
            // 沒有對應的'Cookies'時
            if (accountSession == null)
            {
                return View();
            }
            else
            {
                var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(m => m.AccountID.ToString() == accountSession);
                // 有對應的'Cookies'但值不符合(可能被client修改過)
                if (result == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(Space), new { accountName = result.AccountName});
                }
            }
        }
        
        // 對應'註冊'的檢視
        public IActionResult SignUp()
        {
            return View();
        }

        // 對應'登入'的檢視
        public IActionResult LogIn()
        {
            return View();
        }

        // 消費者的個人空間，預計配合登入系統做調整
        // 保持登入的方式會採取 'Cookies - Session'
        public async Task<IActionResult> Space(string user)
        {
            var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(m => m.AccountName == user);

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
