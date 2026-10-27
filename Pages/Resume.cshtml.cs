using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using aspnetWebApp.Models;
using aspnetWebApp.Services;

namespace aspnetWebApp.Pages;

[IgnoreAntiforgeryToken]
public class ResumeModel : PageModel
{
    private readonly IStudentService _studentService;
    private readonly ILogger<ResumeModel> _logger;

    public ResumeModel(IStudentService studentService, ILogger<ResumeModel> logger)
    {
        _studentService = studentService;
        _logger = logger;
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

    public async Task<IActionResult> OnPostDeleteAsync([FromQuery] int id)
    {
        _logger.LogInformation($"OnPostDeleteAsync вызван с id: {id}");
        await _studentService.DeleteStudentAsync(id);
        _logger.LogInformation($"Студент {id} успешно удалён");
        return new JsonResult(new { success = true });
    }

    
}