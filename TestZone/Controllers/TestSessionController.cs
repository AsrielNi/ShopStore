using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestZone.Data;
using TestZone.Models;

namespace TestZone.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestSessionController : ControllerBase
    {
        public readonly TestContext _testContext;
        public TestSessionController(TestContext testContext)
        {
            _testContext = testContext;
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<string> TestCookies(string name)
        {
            var result = await _testContext.UserSession.FirstOrDefaultAsync(x => x.Name == name);

            if (result == null)
            {
                Guid guid = Guid.NewGuid();
                UserSessionModel model = new UserSessionModel(guid, name);
                _testContext.UserSession.Add(model);
                await _testContext.SaveChangesAsync();

                HttpResponse response = HttpContext.Response;
                response.Cookies.Append("TestSessionID", guid.ToString());
                return "Created SessionID";
            }
            else
            {
                return result.SessionID.ToString();
            }
        }
    }
}
