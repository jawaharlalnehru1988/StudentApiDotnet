namespace StudentApi.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int Age { get; set; }
}