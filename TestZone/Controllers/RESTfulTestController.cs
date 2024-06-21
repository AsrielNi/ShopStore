using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestZone.Data;
using TestZone.Models;

namespace TestZone.Controllers
{

    // 測試用的控制器，測試完成的控制器會移動至'ShopApplication'的專案裡
    // 按照'RESTful'的設計風格
    // 內容可能隨時會改變
    [ApiController]
    [Route("[Controller]")]
    public class RESTfulTestController : ControllerBase
    {
        public readonly TestContext _testContext;
        public RESTfulTestController(TestContext testContext)
        {
            _testContext = testContext;
        }

        // 測試從資料庫裡面取得資料
        [HttpGet]
        public async Task<IActionResult> GetData([FromQuery]AccountModelDTO modelDTO)
        {
            var result = await _testContext.Account.FirstOrDefaultAsync(m =>
                m.AccountName == modelDTO.AccountName &&
                m.AccountPassword == modelDTO.AccountPassword);
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
        public async Task<IActionResult> AddData(AccountModelDTO modelDTO)
        {
            if (ModelState.IsValid)
            {
                var model = new AccountModel(modelDTO);
                _testContext.Account.Add(model);
                await _testContext.SaveChangesAsync();
                return CreatedAtAction(nameof(AddData), new {id = model.AccountID}, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // 測試將新的資料更新至資料庫
        [HttpPut]
        public async Task<IActionResult> UpdateData(string name, string password, string new_password)
        {
            if (ModelState.IsValid)
            {
                var result = await _testContext.Account.FirstOrDefaultAsync(m =>
                    m.AccountName == name &&
                    m.AccountPassword == password);

                if (result == null)
                {
                    return NotFound();
                }

                result.AccountPassword = new_password;
                _testContext.Update(result);
                await _testContext.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // 測試將目標資料從資料庫刪除
        [HttpDelete]
        public async Task<IActionResult> DeleteData(string name, string password)
        {
            var result = await _testContext.Account.FirstOrDefaultAsync(m =>
                m.AccountName == name &&
                m.AccountPassword == password);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                _testContext.Account.Remove(result);
                await _testContext.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
