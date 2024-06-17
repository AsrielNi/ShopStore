using Microsoft.AspNetCore.Mvc;

namespace ShopApplication.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
