using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aspnetWebApp.Models;
using aspnetWebApp.Services;

namespace aspnetWebApp.Pages;

public class IndexModel : PageModel
{
    private readonly IStudentService _studentService;

    public IndexModel(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [BindProperty]
    public Student InputStudent { get; set; } = new();

    public List<Student> StudentsList { get; set; } = new();

    public async Task OnGetAsync()
    {
        StudentsList = await _studentService.GetAllStudentsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            StudentsList = await _studentService.GetAllStudentsAsync();
            return Page();
        }

        await _studentService.CreateStudentAsync(InputStudent);
        
        var technologiesString = InputStudent.Technologies?.Length > 0 
            ? string.Join(", ", InputStudent.Technologies) 
            : "Не выбраны";

        var responseData = new 
        {
            success = true,
            message = "Студент добавлен успешно",
            data = new 
            {
                name = InputStudent.Name,
                phone = InputStudent.Phone,
                email = InputStudent.Email,
                speciality = InputStudent.Speciality,
                course = InputStudent.Course,
                birthDate = InputStudent.BirthDate,
                technologies = technologiesString
            }
        };

        return new JsonResult(responseData);
    }
}