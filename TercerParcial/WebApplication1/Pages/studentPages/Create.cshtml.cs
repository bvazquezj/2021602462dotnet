using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.utils;

namespace WebApplication1.Pages.studentPages;
public class CreateModel : PageModel
{
    private readonly SchoolDataContext _context;

    [BindProperty]
    public model.Student Student { get; set; }

    public CreateModel(SchoolDataContext context)
    {
        _context = context;
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Students.Add(Student);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}