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

        // 從'LogInAPI'的'DataBase'中，取得對應的資料
        [HttpGet]
        public async Task<ActionResult> ReadData(string name)
        {
            var result = await _registrantContext.RegistrantData.FirstOrDefaultAsync(m => m.Name == name);
            return Ok(result);
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
                serverResponse.Add("status", "true");
                serverResponse.Add("message", "successful log in.");
                return Ok(serverResponse);
            }
            serverResponse.Add("status", "false");
            serverResponse.Add("message", "name or password is worng.");
            return BadRequest(serverResponse);
        }
    }
}
