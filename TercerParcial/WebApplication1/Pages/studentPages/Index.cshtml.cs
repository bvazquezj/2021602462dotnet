using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.utils;

namespace WebApplication1.Pages.studentPages;

public class IndexModel : PageModel
{
    
    private readonly SchoolDataContext _context;

    public IndexModel(SchoolDataContext context)
    {
        _context = context;
    }

    public List<model.Student> Students { get; set; }

    public async Task OnGetAsync()
    {
        Students = await _context.Students.ToListAsync();
    }
}