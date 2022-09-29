using EasyCaching.Demo.Interceptors.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasyCaching.Demo.Interceptors.Controllers
{
    [ApiController]
    [Route("api")]
    public class MemoryCacheController : ControllerBase
    {
        private readonly IStoreService _service;

        public MemoryCacheController(IStoreService service)
        {
            _service = service;
        }

        [HttpGet("memory-cache/products/{id}")]
        public Product GetCachedProduct(int id)
        {
            var response = _service.GetCachedProduct(id);

            return response;
        }

        [HttpGet("memory-cache-using-aspects/products/{id}")]
        public Product GetProduct(int id)
        {
            var response = _service.GetProduct(id);

            return response;
        }

        [HttpPut("memory-cache-using-aspects/products/{id}")]
        public Product PutProduct(int id)
        {
            var response = _service.PutProduct(new Product(id));

            return response;
        }

        [HttpDelete("memory-cache-using-aspects/products/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _service.DeleteProduct(id);

            return Ok();
        }
    }
}
