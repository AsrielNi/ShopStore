using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Areas.LogInSystem.Data;
using ShopApplication.Areas.LogInSystem.Models;

namespace ShopApplication.Areas.LogInSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        public readonly LogInContext _logInContext;
        public AuthenticationController(LogInContext logInContext)
        {
            _logInContext = logInContext;
        }

        // 登入系統的首頁
        public async Task<IActionResult> Index()
        {
            HttpRequest request = HttpContext.Request;
            string? accountSession = request.Cookies["UserSessionID"];

            if (accountSession == null)
            {
                return View();
            }
            else
            {
                var result = await _logInContext.Registrants.FirstOrDefaultAsync(m => m.AccountID.ToString() == accountSession.ToUpper());
                // 有對應的'Cookies'但值不符合(可能被client修改過)
                if (result == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Space", "Account", new { area = "", user = result.Name });
                }
            }
        }

        // 登入系統的'登入'頁面
        public async Task<IActionResult> LogIn()
        {
            HttpRequest request = HttpContext.Request;
            string? accountSession = request.Cookies["UserSessionID"];

            if (accountSession == null)
            {
                return View();
            }
            else
            {
                var result = await _logInContext.Registrants.FirstOrDefaultAsync(m => m.AccountID.ToString() == accountSession.ToUpper());
                // 有對應的'Cookies'但值不符合(可能被client修改過)
                if (result == null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Space", "Account", new { area = "", user = result.Name });
                }
            }
        }

        // 登入系統的'註冊'頁面
        public IActionResult SignUp()
        {
            return View();
        }

        // 登出帳戶的功能
        // 透過賦值空字串給Cookies["UserSessionID"]和極短的有效期限來消除Client的指定Cookies。
        public IActionResult LogOut()
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMilliseconds(1)
            };

            HttpResponse response = HttpContext.Response;
            response.Cookies.Append("UserSessionID", "", options);

            return RedirectToAction("Index");
        }

        // 對應'SignUp()'的檢查方法
        [HttpPost]
        public async Task<bool> VerifySignUp(RegistrantsModelDTO modelDTO)
        {

            var result = await _logInContext.Registrants.FirstOrDefaultAsync(m => m.Name == modelDTO.Name);
            if (result == null) // 檢查是否有相同的姓名
            {
                RegistrantsModel model = new RegistrantsModel(modelDTO);
                _logInContext.Add(model);
                await _logInContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        // 對應'LogIn()'的檢查方法
        [HttpPost]
        public async Task<IActionResult> VerifyLogIn(string name, string password)
        {
            var result = await _logInContext.Registrants.FirstOrDefaultAsync(m =>
                m.Name == name &&
                m.Password == password);

            if (result != null)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };

                // 給予客戶端 Cookies["UserSessionID"]
                HttpResponse response = HttpContext.Response;
                response.Cookies.Append("UserSessionID", result.AccountID.ToString(), options);

                return RedirectToAction("Space", "Account", new { area = "", user = result.Name });
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
    }
}
