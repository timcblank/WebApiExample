using Services;
using System.Web.Http;

namespace Controllers
{
    public class HomeController : ApiController
    {
        private readonly IProductService _productService;
        private readonly ILogger _logger;


        public HomeController(ILogger logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = _productService.GetProductsByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
