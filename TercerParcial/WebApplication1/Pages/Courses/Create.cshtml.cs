using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.model;
using WebApplication1.utils;

namespace WebApplication1.Pages_Courses
{
    public class CreateModel : PageModel
    {
        private readonly WebApplication1.utils.SchoolDataContext _context;

        public CreateModel(WebApplication1.utils.SchoolDataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProfessorId"] = new SelectList(_context.Professors, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
