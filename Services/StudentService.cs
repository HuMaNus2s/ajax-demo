using System.Collections.Concurrent;
using aspnetWebApp.Models;

namespace aspnetWebApp.Services;

public class StudentService : IStudentService
{
    private static readonly ConcurrentDictionary<int, Student> _students = new();
    private static int _nextId = 1;

    public StudentService()
    {
    }

    public Task<List<Student>> GetAllStudentsAsync()
    {
        return Task.FromResult(_students.Values.ToList());
    }

    public Task<Student?> GetStudentByIdAsync(int id)
    {
        _students.TryGetValue(id, out var student);
        return Task.FromResult(student);
    }

    public Task CreateStudentAsync(Student student)
    {
        student.Id = _nextId++;
        _students.TryAdd(student.Id, student);
        return Task.CompletedTask;
    }

    public Task UpdateStudentAsync(int id, Student updatedStudent)
    {
        if (_students.ContainsKey(id))
        {
            updatedStudent.Id = id;
            _students[id] = updatedStudent;
        }
        return Task.CompletedTask;
    }

    public Task DeleteStudentAsync(int id)
    {
        _students.TryRemove(id, out _);
        return Task.CompletedTask;
    }
}