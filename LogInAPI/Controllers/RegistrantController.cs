using Microsoft.AspNetCore.Mvc;
using LogInAPI.Data;
using LogInAPI.Models;
using LogInAPI.Library;
using Microsoft.EntityFrameworkCore;

namespace LogInAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrantController : ControllerBase
    {
        public readonly RegistrantContext _registrantContext;
        public RegistrantController(RegistrantContext registrantContext)
        {
            _registrantContext = registrantContext;
        }

        // 透過'Cookies - Session'的方式，直接從'LogInAPI'的'DataBase'中，取得對應的資料
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> FastGetData()
        {
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            HttpRequest request = HttpContext.Request;
            string? sessionID = Request.Cookies[Setting.SessionTag];
            // 如果使用者有對應的'Cookies'時
            if (sessionID != null)
            {
                var result = await _registrantContext.RegistrantData.FirstOrDefaultAsync(m => m.SerialID.ToString() == sessionID);
                // 如果使用者的'Cookies'沒有被修改時
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    serverResponse.Add("status", "false");
                    serverResponse.Add("message", "please log in.");
                    return BadRequest(serverResponse);
                }
            }
            serverResponse.Add("status", "false");
            serverResponse.Add("message", "please log in.");
            return BadRequest(serverResponse);

        }

        // 將資料儲存至'LogInAPI'的'DataBase'中
        [HttpPost]
        public async Task<ActionResult> CreateData([FromForm]RegistrantDTO modelDTO)
        {
            // 儲存欲回傳之資訊，讓前端之javascript使用
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            // 檢查使用者的名字是否重複
            var result = await _registrantContext.RegistrantData.FirstOrDefaultAsync(m => m.Name == modelDTO.Name);
            if (result == null)
            {
                string newID;
                while (true)
                {
                    // 產生隨機16字元的代號
                    newID = GenerateSomething.GenerateRegistrantID(16);
                    // 檢查代號是否重複
                    var idResult = await _registrantContext.RegistrantData.FirstOrDefaultAsync(m => m.RegistrantID == newID);
                    if (idResult == null)
                    {
                        break;
                    }
                }
                var model = new Registrant(modelDTO, newID);
                _registrantContext.RegistrantData.Add(model);
                await _registrantContext.SaveChangesAsync();

                serverResponse.Add("status", "true");
                serverResponse.Add("message", "Successful Sign Up.");
                return CreatedAtAction(nameof(CreateData), serverResponse);
            }
            serverResponse.Add("status", "false");
            serverResponse.Add("message", "Repeat name.");
            return CreatedAtAction(nameof(CreateData), serverResponse);
        }

        // 提供使用者登入用的方法
        // 這裡會提供使用者對應的'Cookies'
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> LogIn([FromForm]string name, [FromForm]string password)
        {
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            //檢查帳號和密碼是否是正確的
            var result = await _registrantContext.RegistrantData.FirstOrDefaultAsync(m =>
                m.Name == name &&
                m.Password == password);

            if (result != null)
            {
                HttpResponse response = HttpContext.Response;
                response.Cookies.Append(Setting.SessionTag, GenerateSomething.GenerateUpperGuid(result.SerialID), Setting.DefaultCookiesOptions);

                serverResponse.Add("status", "true");
                serverResponse.Add("message", "successful log in.");
                serverResponse.Add("RegistrantID", result.RegistrantID);
                return Ok(serverResponse);
            }
            serverResponse.Add("status", "false");
            serverResponse.Add("message", "name or password is worng.");
            return BadRequest(serverResponse);
        }

        // 登入系統的登出功能
        [HttpGet]
        [Route("[Action]")]
        public ActionResult LogOut()
        {
            HttpResponse response = HttpContext.Response;
            CookieOptions cancelCookiesOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMicroseconds(1),
            };

            response.Cookies.Append(Setting.SessionTag, "", cancelCookiesOptions);
            return Ok();
        }
    }
}
