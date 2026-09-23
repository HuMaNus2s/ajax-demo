using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspnetWebApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";
    
    [BindProperty]
    public string Phone { get; set; } = "";
    
    [BindProperty]
    public string Email { get; set; } = "";
    
    [BindProperty]
    public string Speciality { get; set; } = "";
    
    [BindProperty]
    public string Course { get; set; } = "";
    
    [BindProperty]
    public string BirthDate { get; set; } = "";
    
    [BindProperty]
    public string[] Technologies { get; set; } = Array.Empty<string>();

    public void OnGet()
    { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { success = false, message = "Ошибка валидации", errors = errors });
        }

        var technologiesString = Technologies.Length > 0 
            ? string.Join(", ", Technologies) 
            : "Не выбраны";

        var responseData = new 
        {
            success = true,
            data = new 
            {
                name = Name,
                phone = Phone,
                email = Email,
                speciality = Speciality,
                course = Course,
                birthDate = BirthDate,
                technologies = technologiesString
            }
        };

        return new JsonResult(responseData);
    }
}