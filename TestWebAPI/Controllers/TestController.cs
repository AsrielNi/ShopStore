using Microsoft.AspNetCore.Mvc;
using ShopApplication.Data;
using ShopApplication.Models;

namespace TestWebAPI.Controllers
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
        [HttpPost]
        public async Task<ActionResult> AddData(Product model)
        {
            _shopContext.ProductData.Add(model);
            await _shopContext.SaveChangesAsync();
            return CreatedAtAction(nameof(AddData), model);
        }
    }
}
