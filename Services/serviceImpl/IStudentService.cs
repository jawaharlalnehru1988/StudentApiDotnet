using StudentApi.Models; // Add this directive
using StudentApi.DTOs;

namespace StudentApi.Services.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto?> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(StudentDto studentDto); // This expects a Student return type
    Task<bool> UpdateStudentAsync(int id, StudentDto studentDto);
    Task<bool> DeleteStudentAsync(int id);
}

