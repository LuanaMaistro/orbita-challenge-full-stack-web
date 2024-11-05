using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 
using OrbitaChallengeBackEnd.Data;
using OrbitaChallengeBackEnd.Interfaces;
using OrbitaChallengeBackEnd.Models;
using OrbitaChallengeBackEnd.ViewModels;

using System.Net;
using System.Threading.Tasks;

namespace OrbitaChallengeBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: Students/GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentRepository.GetAllAsync();
            return Ok(result);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {       
                return BadRequest(new ResultViewModel<bool>(false, "Invalid data provided."));
            }

            var result = await _studentRepository.AddAsync(student); 

            return Ok(result);
        }

        // POST: Students/Edit
        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<bool>(false, "Invalid data provided.")); 
            }

            var result = await _studentRepository.EditAsync(student); 
            return Ok(result); 
        }

        [HttpGet("search")]
        public async Task<ActionResult<ResultViewModel<Student>>> GetBy([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest(new ResultViewModel<Student>(false, "Search term cannot be empty."));
            }

            var result = await _studentRepository.GetByAsync(searchTerm);
            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // POST: Students/Delete
        [HttpPost("delete/{ra}")]
        public async Task<IActionResult> Delete(string ra)
        {
            var result = await _studentRepository.DeleteAsync(ra); 
            return Ok(result); 
        }
    }
}
