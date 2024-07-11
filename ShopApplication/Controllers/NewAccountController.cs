using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.Controllers
{
    public class NewAccountController : Controller
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
    }
}
