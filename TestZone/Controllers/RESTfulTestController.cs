using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;

namespace TestZone.Controllers
{

    // 測試用的控制器，測試完成的控制器會移動至'ShopApplication'的專案裡
    // 按照'RESTful'的設計風格
    // 內容可能隨時會改變
    [ApiController]
    [Route("[Controller]")]
    public class RESTfulTestController : ControllerBase
    {
        public readonly ShopContext _shopContext;
        public RESTfulTestController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        // 測試從資料庫裡面取得資料
        [HttpGet]
        public async Task<IActionResult> GetData(string name)
        {
            var result = await _shopContext.CustomerInfo.FirstOrDefaultAsync(m => m.CustomerName == name);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // 測試將資料寫入至資料庫
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

        // 測試將新的資料更新至資料庫
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

        // 測試將目標資料從資料庫刪除
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
