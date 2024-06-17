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
                return CreatedAtAction(nameof(AddData), new {id = model.CustomerID}, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(CustomerInfoModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _shopContext.CustomerInfo.FirstOrDefaultAsync();

                _shopContext.Entry(result).State = EntityState.Detached;
                model.CustomerID = result.CustomerID;
                _shopContext.Entry(model).State = EntityState.Modified;

                _shopContext.Update(model);
                await _shopContext.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(int customerID)
        {
            var result = await _shopContext.CustomerInfo.FindAsync(customerID);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                _shopContext.CustomerInfo.Remove(result);
                await _shopContext.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
