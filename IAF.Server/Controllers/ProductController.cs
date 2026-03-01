using IAF.Server.Domain.DTO;
using IAF.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAF.Server.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("details")]
        public async Task<ActionResult<ProductDto>> GetAllProducts()
        {
            var response = await _service.GetAllProductsAsync();
            return StatusCode(response.ResponseDefaultDto.StatusCode, response);
        }

        [HttpPost("produce")]
        public async Task<ActionResult<ResponseDefaultDto>> ProduceProduct([FromBody] ProduceDto request)
        {
            var response = await _service.ProduceProductAsync(request);
            return StatusCode(response.StatusCode, response);
        }
    }
}
