using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.Controllers
{
    public class AccountSettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonInfo()
        {
            return View();
        }
    }
}
