using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.DTOs;
using StudentApi.Services.Interfaces;
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
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _studentService.GetAllStudentsAsync();

        // Map Student to StudentDto
        var studentDtos = students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age
        });

        return Ok(studentDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();

        // Map Student to StudentDto
        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age
        };

        return Ok(studentDto);
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> Create(StudentDto studentDto)
    {
        // Map StudentDto to Student
        var student = new Student
        {
            Name = studentDto.Name,
            Age = studentDto.Age
        };

        var createdStudent = await _studentService.CreateStudentAsync(studentDto);

        // Map created Student to StudentDto
        var createdStudentDto = new StudentDto
        {
            Id = createdStudent.Id,
            Name = createdStudent.Name,
            Age = createdStudent.Age
        };

        return CreatedAtAction(nameof(GetById), new { id = createdStudentDto.Id }, createdStudentDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, StudentDto studentDto)
    {
        // Map StudentDto to Student
        var student = new Student
        {
            Id = id,
            Name = studentDto.Name,
            Age = studentDto.Age
        };

        var success = await _studentService.UpdateStudentAsync(id, studentDto);
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
