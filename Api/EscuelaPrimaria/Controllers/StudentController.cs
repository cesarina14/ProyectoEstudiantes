using EscuelaPrimaria.Request;
using EscuelaPrimaria.Response;
using EscuelaPrimaria.Service;
using Microsoft.AspNetCore.Mvc;

namespace EscuelaPrimaria.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student/list?search=juan
        [HttpGet("listAll")]
        public async Task<ActionResult<StudentResponse>> List()
        {
            var result = await _studentService.ListAll();
            return Ok(result);
        }
        [HttpGet("listToAttendenceList")]
        public async Task<ActionResult<StudentResponse>> listToAttendenceList()
        {
            var result = await _studentService.listToAttendenceList();
            return Ok(result);
        }
        // GET: api/student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponse>> Get(long id)
        {
            var result = await _studentService.Find(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        // POST: api/student
        [HttpPost("create")]
        public async Task<ActionResult<StudentResponse>> Create(StudentRequest request)
        {
            var result = await _studentService.Add(request);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        // PUT: api/student
        [HttpPut("update")]
        public async Task<ActionResult<StudentResponse>> Update(StudentRequest request)
        {
            var result = await _studentService.Update(request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        // DELETE: api/student/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentResponse>> Delete(long id)
        {
            var result = await _studentService.Delete(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
        [HttpGet("listCalificationSummary")]
        public async Task<ActionResult<StudentResponse>> listCalificationSummary()
        {
            var result = await _studentService.getCalificationSummary();
            return Ok(result);
        }
    }
}


