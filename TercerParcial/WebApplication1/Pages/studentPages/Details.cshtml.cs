using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.model;
using WebApplication1.utils;

public class DetailsModel : PageModel
{
    private readonly SchoolDataContext _context;

    public Student Student { get; set; }

    public DetailsModel(SchoolDataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Student = await _context.Students.FindAsync(id);
        if (Student == null) return NotFound();
        return Page();
    }
}