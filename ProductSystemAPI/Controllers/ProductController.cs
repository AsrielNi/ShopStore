using Microsoft.AspNetCore.Mvc;
using ProductSystemAPI.Data;
using ProductSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductController : ControllerBase
    {
        public readonly ProductContext _productContext;

        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return NotFound();
        }

        [HttpPost]
        [Route("[Action]")]
        public async Task<ActionResult> CreateData(Product model)
        {
            _productContext.ProductInfo.Add(model);
            await _productContext.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateData), model);
        }

        [HttpGet]
        [Route("[Action]")]
        public ActionResult<IQueryable<Product>> GetMenu()
        {
            var results = _productContext.ProductInfo.Take(20);
            if (results != null)
            {
                foreach (var item in results)
                {
                    item.PicturePath = $"/{APItoLINK._apiName}/" + item.PicturePath;
                }
                return Ok(results);
            }
            return NotFound();
        }

        // 顯示個別商品的資訊
        // 未來考量是否繼續使用路由屬性或是查詢字串
        [HttpGet]
        [Route("[Action]/{productID}")]
        public async Task<ActionResult<Product>> GetProductInfo(string productID)
        {
            var result = await _productContext.ProductInfo.FirstOrDefaultAsync(m => m.ProductID == productID);
            if (result != null)
            {
                result.PicturePath = $"/{APItoLINK._apiName}/" + result.PicturePath;
                return Ok(result);
            }
            return NotFound();
        }
    }
}
