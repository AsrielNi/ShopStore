using Microsoft.AspNetCore.Mvc;

namespace LogInSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
