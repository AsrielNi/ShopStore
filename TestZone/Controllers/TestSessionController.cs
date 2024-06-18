using Microsoft.AspNetCore.Mvc;

namespace TestZone.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestSessionController : ControllerBase
    {

        [HttpGet]
        [Route("[Action]")]
        public string TestCookies()
        {
            Guid guid = Guid.NewGuid();
            HttpResponse response = HttpContext.Response;
            response.Cookies.Append("Test", guid.ToString());
            return "Hello";
        }
    }
}
