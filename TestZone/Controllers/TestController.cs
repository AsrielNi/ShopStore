using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

namespace TestZone.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        public readonly ShopContext _shopContext;
        public TestController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetData(string name)
        {
            var result = _shopContext.CustomerInfo.FirstOrDefaultAsync(m => m.CustomerName == name);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddData(CustomerInfoModel model)
        {
            if (ModelState.IsValid)
            {
                _shopContext.CustomerInfo.Add(model);
                await _shopContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
