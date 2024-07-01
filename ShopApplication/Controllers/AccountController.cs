using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;
using ShopApplication.Areas.LogInSystem.Data;

namespace ShopApplication.Controllers
{
    // 處理消費者資料用的控制器
    public class AccountController : Controller
    {
        // 儲存商家資料的資料庫
        public readonly ShopContext _shopContext;
        // 儲存帳戶資料的資料庫
        public readonly LogInContext _logInContext;
        
        // 控制器的初始化建構式
        public AccountController(ShopContext shopContext, LogInContext logInContext)
        {
            _shopContext = shopContext;
            _logInContext = logInContext;
        }
        
        // 預設的首頁，目前沒有特別的規劃
        public async Task<IActionResult> Index()
        {
            HttpRequest request = HttpContext.Request;
            string? accountSession = request.Cookies["UserSessionID"];

            // 沒有對應的'Cookies'時
            if (accountSession == null)
            {
                return View();
            }
            else
            {
                var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(m => m.AccountID.ToString() == accountSession.ToUpper());
                // 有對應的'Cookies'但值不符合(可能被client修改過)
                if (result == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(Space), new { user = result.AccountName});
                }
            }
        }

        // 消費者的個人空間，預計配合登入系統做調整
        // 保持登入的方式會採取 'Cookies - Session'
        public async Task<IActionResult> Space(string user)
        {
            var result = await _logInContext.Registrants.FirstOrDefaultAsync(m => m.Name == user);

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
