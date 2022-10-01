using EasyCaching.Demo.Interceptors.Services;
using EasyCaching.Demo.Web.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyCaching.Demo.Interceptors.Controllers
{
    [ApiController]
    [Route("api")]
    public class RedisStoreController : ControllerBase
    {
        private readonly IStoreService _service;

        public RedisStoreController(IStoreService service)
        {
            _service = service;
        }

        [HttpGet("redis-cache/categories/{id}")]
        public Category GetCachedCategory(int id)
        {
            var response = _service.GetCachedCategory(id);

            return response;
        }

        [HttpGet("redis-cache/categories/{id}/raw")]
        public string GetRawCachedCategory(int id)
        {
            var response = _service.GetRawCacheCategory(id);

            return response;
        }

        [HttpGet("redis-cache-using-aspects/categories/{id}")]
        public Category GetCategory(int id)
        {
            var response = _service.GetCategory(id);

            return response;
        }

        [HttpPut("redis-cache-using-aspects/categories/{id}")]
        public Category PutCategory(int id)
        {
            var response = _service.PutCategory(new Category(id));

            return response;
        }

        [HttpDelete("redis-cache-using-aspects/categories/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _service.DeleteCategory(id);

            return Ok();
        }
    }
}
