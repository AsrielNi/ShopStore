using Microsoft.AspNetCore.Mvc;

namespace ProductSystemAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        public ActionResult Index()
        {
            return NotFound();
        }
    }
}
