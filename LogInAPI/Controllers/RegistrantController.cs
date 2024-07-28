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
        public readonly LogInContext _loginContext;

        // 對應 AddDbContext<LogInContext> 的建構式
        public RegistrantController(LogInContext loginContext)
        {
            _loginContext = loginContext;
        }

        // 透過'Cookies - Session'的方式，直接從'LogInAPI'的'DataBase'中，取得對應的資料
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> FastGetData()
        {
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            HttpRequest request = HttpContext.Request;
            string? sessionID = Request.Cookies[ApiSetting.SessionTag];
            // 如果使用者有對應的'Cookies'時
            if (sessionID != null)
            {
                var result = await _loginContext.AccountData.FirstOrDefaultAsync(m => m.SessionID.ToString() == sessionID);
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
        [Route("[Action]")]
        public async Task<ActionResult> SignUp([FromForm]AccountModelDTO modelDTO)
        {
            // 儲存欲回傳之資訊，讓前端之javascript使用
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            // 檢查帳戶的名字是否重複
            var result = await _loginContext.AccountData.FirstOrDefaultAsync(m => m.AccountName == modelDTO.AccountName);
            if (result == null)
            {
                while (true)
                {
                    var newAccountModel = new AccountModel(modelDTO);
                    // 檢查新建立的AccountModel中的AccountID是否重複於資料庫
                    var checkResult = await _loginContext.AccountData.FirstOrDefaultAsync(m => m.AccountID == newAccountModel.AccountID);
                    // 如果沒有重複的話
                    if (checkResult == null)
                    {
                        // 將新的AccountModel寫入至資料庫並脫離迴圈
                        _loginContext.AccountData.Add(newAccountModel);
                        await _loginContext.SaveChangesAsync();
                        break;
                    }
                }
                serverResponse.Add("status", "true");
                serverResponse.Add("message", "Successful Sign Up.");
                return CreatedAtAction(nameof(SignUp), serverResponse);
            }
            else
            {
                serverResponse.Add("status", "false");
                serverResponse.Add("message", "Repeat name.");
                return BadRequest(serverResponse);
            }
            
        }

        // 提供使用者登入用的方法
        // 這裡會提供使用者對應的'Cookies'
        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> LogIn([FromForm]string name, [FromForm]string password)
        {
            Dictionary<string, string> serverResponse = new Dictionary<string, string>();
            //檢查帳號和密碼是否是正確的
            var result = await _loginContext.AccountData.FirstOrDefaultAsync(m =>
                m.AccountName == name &&
                m.Password == password);
            // 如果帳號和密碼都正確的話
            if (result != null)
            {
                Console.WriteLine(result.SessionID);
                // 給予特定的Cookies作為登入之依據
                HttpResponse response = HttpContext.Response;
                response.Cookies.Append(
                    ApiSetting.SessionTag, result.SessionID.ToString().ToUpper(), ApiSetting.DefaultCookiesOptions);

                serverResponse.Add("status", "true");
                serverResponse.Add("message", "successful log in.");
                serverResponse.Add("RegistrantID", result.AccountID);
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
            // 重新設置登入依據的Cookies，將其設置為空字串並存在極短時間，讓其被自動消除
            HttpResponse response = HttpContext.Response;
            CookieOptions cancelCookiesOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMicroseconds(1),
            };

            response.Cookies.Append(ApiSetting.SessionTag, "", cancelCookiesOptions);
            return Ok();
        }
    }
}
