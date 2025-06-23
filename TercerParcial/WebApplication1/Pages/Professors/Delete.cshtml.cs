using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.model;
using WebApplication1.utils;

namespace WebApplication1.Pages_Professors
{
    public class DeleteModel : PageModel
    {
        private readonly WebApplication1.utils.SchoolDataContext _context;

        public DeleteModel(WebApplication1.utils.SchoolDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Professor Professor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FirstOrDefaultAsync(m => m.Id == id);

            if (professor is not null)
            {
                Professor = professor;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors.FindAsync(id);
            if (professor != null)
            {
                Professor = professor;
                _context.Professors.Remove(Professor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
