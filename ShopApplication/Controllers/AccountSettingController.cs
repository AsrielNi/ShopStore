using Microsoft.AspNetCore.Mvc;
using ShopApplication.Areas.LogInSystem.Models;
using ShopApplication.Areas.LogInSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace ShopApplication.Controllers
{
    public class AccountSettingController : Controller
    {
        public readonly LogInContext _logInContext;
        public AccountSettingController(LogInContext logInContext)
        {
            _logInContext = logInContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PersonInfo()
        {
            HttpRequest request = HttpContext.Request;
            string accountSession = request.Cookies["UserSessionID"];

            if (accountSession != null)
            {
                var result = await _logInContext.Registrants.FirstOrDefaultAsync(m =>
                    m.AccountID.ToString() == accountSession.ToUpper());
                var resultDTO = new RegistrantsModelDTO(result);
                return View(resultDTO);
            }
            else
            {
                return RedirectToAction("Index", "Authentication", new { area = "LogInSystem" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData(RegistrantsModelDTO modelDTO)
        {
            HttpRequest request = HttpContext.Request;
            string accountSession = request.Cookies["UserSessionID"];
            if (accountSession != null)
            {
                var result = await _logInContext.Registrants.FirstOrDefaultAsync(m =>
                    m.AccountID.ToString() == accountSession.ToUpper());

                _logInContext.Entry(result).State = EntityState.Detached;
                RegistrantsModel model = new RegistrantsModel(modelDTO, result.AccountID);
                _logInContext.Entry(model).State = EntityState.Modified;

                _logInContext.Update(model);
                await _logInContext.SaveChangesAsync();

                return RedirectToAction("PersonInfo");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
