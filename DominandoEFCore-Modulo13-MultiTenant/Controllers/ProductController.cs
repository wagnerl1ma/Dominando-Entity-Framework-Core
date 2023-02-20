using DominandoEFCore_Modulo13_MultiTenant.Data;
using DominandoEFCore_Modulo13_MultiTenant.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DominandoEFCore_Modulo13_MultiTenant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Product> Get([FromServices] ApplicationContext db)
        {
            var product = db.Products.ToArray();

            return product;
        }
    }
}