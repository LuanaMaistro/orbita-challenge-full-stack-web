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
            try
            {
                var result = await _studentRepository.GetAllAsync();

                if (result.Success && result.Data != null)
                    return Ok(result); 

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred while retrieving students.", Details = ex.Message });
            }
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Message = "Invalid data provided."});
                

                var result = await _studentRepository.AddAsync(student);

                if (result.Success)
                    return Ok(result);                

                return Ok(new { Message = result.Message, Details = result.Data });
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Message = "Error adding student.", Details = ex.Message });
            }

        }

        // POST: Students/Edit
        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] Student student)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new ResultViewModel<bool>(false, "Invalid data provided.")); 
                
                var result = await _studentRepository.EditAsync(student);

                if (result.Success)
                    return Ok(result);

                return NoContent();

            }
            catch (Exception ex) {
            
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });

            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<ResultViewModel<Student>>> GetBy([FromQuery] string searchTerm)
        {
            try
            {
                 if (string.IsNullOrWhiteSpace(searchTerm))
                     return BadRequest("Search term cannot be empty.");
                 
                 
                var result = await _studentRepository.GetByAsync(searchTerm);

                 if (!result.Success)
                    return NotFound(result);
                 

                    return Ok(result);
            }
            catch(Exception ex) 
            {
              return BadRequest(new { Message = "An error occurred.", Details = ex.Message }); 
            }
        }

        // POST: Students/Delete
        [HttpPost("delete/{ra}")]
        public async Task<IActionResult> Delete(string ra)
        {
            try
            {
                var result = await _studentRepository.DeleteAsync(ra);
                
                if (!result.Success)
                    return BadRequest(result);

                return Ok(result); 


            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });

            }
        }

        [HttpGet("GetByRA/{ra}")]
        public async Task<ActionResult> GetByRA(string RA)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RA))
                    return BadRequest("RA cannot be empty.");


                var result = await _studentRepository.GetByRAAsync(RA);

                if (!result.Success)
                    return NotFound(result);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "An error occurred.", Details = ex.Message });
            }
        }
    }
}
