using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orbit.BL;
using Orbit.DAL;
using Orbit.Model;

namespace Orbit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentBL _studentBL;
        public StudentsController(IStudentBL studentBL)
        {
            _studentBL = studentBL;
        }

        // GET: api/Students
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return _studentBL.GetStudent();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Student student = await _studentBL.GetStudent(id);
            if (student != null)
                return Ok(student);
            else
                return NotFound();
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }
            Student studentUpdated = await _studentBL.PutStudentAsync(id, student);
            if (studentUpdated == null)
                return BadRequest();
            else
                return Ok(studentUpdated);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool inserted = _studentBL.PostStudentAsync(student);
            if (inserted)
                return CreatedAtAction("GetStudent", new { id = student.Id }, student);
            else
                return BadRequest("Student was not inserted. Check and Try Again");
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _studentBL.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }

            _studentBL.DeleteStudentAsync(student);

            return Ok(student);
        }

    }
}