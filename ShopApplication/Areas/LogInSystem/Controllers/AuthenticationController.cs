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
        public IActionResult Index()
        {
            return View();
        }

        // 登入系統的'登入'頁面
        public IActionResult LogIn()
        {
            return View();
        }

        // 登入系統的'註冊'頁面
        public IActionResult SignUp()
        {
            return View();
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
    }
}
