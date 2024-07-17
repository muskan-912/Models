using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Services;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            await _studentService.CreateStudentAsync(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.StudentId }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }
            await _studentService.UpdateStudentAsync(student);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
