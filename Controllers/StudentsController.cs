using Microsoft.AspNetCore.Mvc;
using StudentApi.Data;
using StudentApi.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create(Student student)
    {
        var createdStudent = await _studentService.CreateStudentAsync(student);
        return CreatedAtAction(nameof(GetById), new { id = createdStudent.Id }, createdStudent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        var success = await _studentService.UpdateStudentAsync(id, student);
        if (!success) return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _studentService.DeleteStudentAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
