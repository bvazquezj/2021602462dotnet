using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.model;
using WebApplication1.utils;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly SchoolDataContext _context;

    [BindProperty]
    public Student Student { get; set; }

    public DeleteModel(SchoolDataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Student = await _context.Students.FindAsync(id);
        if (Student == null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}