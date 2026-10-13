using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aspnetWebApp.Models;
using aspnetWebApp.Services;

namespace aspnetWebApp.Pages;

public class ResumeModel : PageModel
{
    private readonly IStudentService _studentService;

    public ResumeModel(IStudentService studentService)
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

    public async Task<IActionResult> OnGetAllAsync()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return new JsonResult(new { success = true, data = students });
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _studentService.DeleteStudentAsync(id);
        return new JsonResult(new { success = true });
    }

    
}