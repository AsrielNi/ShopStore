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

        // 測試資料庫自動新增'Session'
        // 測試'Cookies'的相關設定(有效期限等...)
        [HttpGet]
        [Route("[Action]")]
        public async Task<string> TestCookies(string name)
        {
            var result = await _testContext.UserSession.FirstOrDefaultAsync(x => x.Name == name);

            if (result == null)
            {
                Guid guid = Guid.NewGuid();
                // Session紀錄在資料庫裡
                UserSessionModel model = new UserSessionModel(guid, name);
                _testContext.UserSession.Add(model);
                await _testContext.SaveChangesAsync();

                // 設置'Cookies'的參數
                CookieOptions cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddSeconds(30)
                };

                // 這裡的'Response'指的是'Server'端的回應，透過'Cookies'保留'Server'端想要給'Client'的資料
                HttpResponse response = HttpContext.Response;
                response.Cookies.Append("TestSessionID", guid.ToString(), cookieOptions);
                return "Created SessionID";
            }
            else
            {
                return result.SessionID.ToString();
            }
        }

        // 測試如何重置'Cookies'資訊
        // 測試'Cookies'的相關設定(有效期限等...)
        [HttpGet]
        [Route("[Action]")]
        public async Task<string> ResetSessionID(string name)
        {
            HttpRequest request = HttpContext.Request;
            HttpResponse response = HttpContext.Response;

            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddSeconds(30)
            };

            string? sessionID = request.Cookies["TestSessionID"];
            if (sessionID == null)
            {
                var result = await _testContext.UserSession.FirstOrDefaultAsync(x => x.Name == name);
                Guid guid = result.SessionID;
                response.Cookies.Append("TestSessionID", guid.ToString(), cookieOptions);
                return "Reset SessionID";
            }
            else
            {
                return "Exist SessionID";
            }
        }
    }
}
