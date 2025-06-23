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
    public class IndexModel : PageModel
    {
        private readonly WebApplication1.utils.SchoolDataContext _context;

        public IndexModel(WebApplication1.utils.SchoolDataContext context)
        {
            _context = context;
        }

        public IList<Professor> Professor { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Professor = await _context.Professors.ToListAsync();
        }
    }
}
