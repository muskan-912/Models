using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Services;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeachersController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(Teacher teacher)
        {
            await _teacherService.CreateTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.TeacherId }, teacher);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }
            await _teacherService.UpdateTeacherAsync(teacher);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return NoContent();
        }
    }
}
