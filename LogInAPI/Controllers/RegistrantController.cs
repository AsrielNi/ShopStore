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
        public async Task<ActionResult> CreateData(RegistrantDTO modelDTO)
        {
            var newID = GenerateSomething.GenerateRegistrantID(16);
            var model = new Registrant(modelDTO, newID);
            _registrantContext.RegistrantData.Add(model);
            await _registrantContext.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateData), model);
        }
    }
}
