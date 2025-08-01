using Microsoft.AspNetCore.Mvc;
using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;
using EscuelaPrimaria.Service.NewFolder;
using EscuelaPrimaria.Service;

namespace EscuelaPrimaria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceeService _attendenceService;

        public AttendenceController(IAttendenceeService attendenceService)
        {
            _attendenceService = attendenceService;
        }

        [HttpPost("SaveAttedenceBatch")]
        public async Task<ActionResult<AttendenceResponse>> SaveBatchAttendence(List<AttendenceRequest> request)
        {
            var response = await _attendenceService.Add(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<AttendenceResponse>> Update([FromBody] AttendenceRequest request)
        {
           
            var response = await _attendenceService.Update(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("remove")]
        public async Task<ActionResult<AttendenceResponse>> Delete(long id)
        {
            var response = await _attendenceService.Delete(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<AttendenceResponse>> Find(long id)
        {
            var response = await _attendenceService.Find(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("listAll")]
        public async Task<ActionResult<AttendenceResponse>> List()
        {
            var response = await _attendenceService.ListAll();
            return Ok(response);
        }
    }
}
