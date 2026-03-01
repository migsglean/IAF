using IAF.Server.Domain.DTO;
using IAF.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAF.Server.Controllers
{
    [ApiController]
    [Route("api/parts")]
    public class PartsController : ControllerBase
    {
        private readonly IPartsService _partsService;

        public PartsController(IPartsService partsService)
        {
            _partsService = partsService;
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetAllParts()
        {
            var response = await _partsService.GetAllPartsAsync();
            return StatusCode(response.ResponseDefaultDto.StatusCode, response);
        }

        [HttpPost("insert")]
        public async Task<IActionResult> AddPart([FromBody] PartsDetails request)
        {
            var response = await _partsService.AddPartAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePart([FromBody] PartsDetails request)
        {
            var response = await _partsService.UpdatePartAsync(request);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("delete/{partsId}")]
        public async Task<IActionResult> DeletePart(string partsId)
        {
            var response = await _partsService.DeletePartAsync(partsId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
