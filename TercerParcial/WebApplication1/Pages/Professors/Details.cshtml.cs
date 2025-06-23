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
    public class DetailsModel : PageModel
    {
        private readonly WebApplication1.utils.SchoolDataContext _context;

        public DetailsModel(WebApplication1.utils.SchoolDataContext context)
        {
            _context = context;
        }

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
    }
}
