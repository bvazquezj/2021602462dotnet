using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.model;
using WebApplication1.utils;

namespace WebApplication1.Pages_Professors
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
            return Page();
        }

        [BindProperty]
        public Professor Professor { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Professors.Add(Professor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
