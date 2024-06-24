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

        [HttpPost]
        public async Task<IActionResult> AccountLogIn(string name, string password)
        {
            var result = await _shopContext.AccountInfo.FirstOrDefaultAsync(
                m => m.AccountName == name &&
                m.AccountPassword == password);

            if (result != null)
            {
                return RedirectToAction("Space", "Account", new { user=$"{name}"});
            }
            else
            {
                return View();
            }
        }
    }
}
