using Microsoft.AspNetCore.Mvc;
using ProductSystemAPI.Data;
using ProductSystemAPI.Models;
using ProductSystemAPI.Library;

namespace ProductSystemAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
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
    }
}
