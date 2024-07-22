using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Space(string id)
        {
            return View();
        }
    }
}
