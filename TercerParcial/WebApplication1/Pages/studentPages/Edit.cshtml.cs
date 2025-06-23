using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.utils;

namespace WebApplication1.Pages.studentPages;

public class EditModel : PageModel
{
    private readonly SchoolDataContext _context;

    [BindProperty]
    public model.Student? Student { get; set; }

    public EditModel(SchoolDataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Student = await _context.Students.FindAsync(id);
        if (Student == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _context.Attach(Student).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}