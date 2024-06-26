using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

namespace ShopApplication.Controllers
{
    // 該控制器目的是驗證其他的'檢視'所提交的資料
    public class VerificationController : Controller
    {
        public readonly ShopContext _shopContext;
        public VerificationController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        // 作為 AccountController - SignUp 的 SignUp.cshtml 的驗證方法
        [HttpPost]
        public async Task<IActionResult> AccountSignUp(AccountInfoModelDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                // 檢查'AccountName'是否有重複
                // 未來應該會將此檢查單獨抽離並使用'SPA'技術
                var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(m => m.AccountName == modelDTO.AccountName);
                if (result == null)
                {
                    AccountInfoModel model = new AccountInfoModel(modelDTO);
                    _shopContext.Add(model);
                    await _shopContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // 作為 AccountController - LogIn 的 LogIn.cshtml 的驗證方法
        // 配合 Ajax 和 jQuery 使用
        // 加入Cookies["UserSessionID"]來作為持續登入的依據
        [HttpPost]
        public async Task<bool> AccountLogIn(string name, string password)
        {
            // 檢查帳號和密碼有登記於資料庫裡
            var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(
                m => m.AccountName == name &&
                m.AccountPassword == password);

            if (result != null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };

                // 給予客戶端 Cookies["UserSessionID"]
                HttpResponse response = HttpContext.Response;
                response.Cookies.Append("UserSessionID", result.AccountID.ToString(), options);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
