using StudentApi.Models;
using StudentApi.Data;
using Microsoft.EntityFrameworkCore;
using StudentApi.DTOs;
using StudentApi.Services.Interfaces;

namespace StudentApi.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _context.Students.ToListAsync();

        // Map each Student to StudentDto
        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age
        });
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return null;

        // Map Student to StudentDto
        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age
        };
    }



    public async Task<Student> CreateStudentAsync(StudentDto studentDto)
    {
        // Map StudentDto to Student
        var student = new Student
        {
            Name = studentDto.Name,
            Age = studentDto.Age
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<bool> UpdateStudentAsync(int id, StudentDto studentDto)
    {
        if (id != studentDto.Id) return false;

        // Map StudentDto to Student
        var student = new Student
        {
            Id = studentDto.Id,
            Name = studentDto.Name,
            Age = studentDto.Age
        };

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}
