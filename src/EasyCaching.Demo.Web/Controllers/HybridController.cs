using EasyCaching.Demo.Web.Services;
using EasyCaching.Demo.Web.Services.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EasyCaching.Demo.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class HybridController : ControllerBase
    {
        private readonly IHybridStore _service;

        public HybridController(IHybridStore service)
        {
            _service = service;
        }

        [HttpGet("hybrid-cache/memory/customer/{id}")]
        public Customer GetCustomerInMemory(int id)
        {
            var response = _service.GetCustomerByMemory(id);

            return response;
        }

        [HttpGet("hybrid-cache/redis/customer/{id}")]
        public Customer GetCustomerInRedis(int id)
        {
            var response = _service.GetCustomerByRedis(id);

            return response;
        }

        [HttpGet("hybrid-cache/hybrid/customer/{id}")]
        public Customer GetCustomerByHybrid(int id)
        {
            var response = _service.GetCustomerByHybrid(id);

            return response;
        }

        [HttpGet("hybrid-cache-using-aspects/customer/{id}")]
        public Customer GetCustomer(int id)
        {
            var response = _service.GetCustomer(id);

            return response;
        }

        [HttpPut("hybrid-cache-using-aspects/customer/{id}")]
        public Customer PutCustomer(int id)
        {
            var response = _service.PutCustomer(new Customer(id));

            return response;
        }

        [HttpDelete("hybrid-cache-using-aspects/categories/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _service.DeleteCustomer(id);

            return Ok();
        }
    }
}
