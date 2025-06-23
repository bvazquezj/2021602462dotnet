using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.DashBoard
{
    [Authorize(Roles = "Profesor")]
    public class ProfesorModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfesorModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Curso> CursosDelProfesor { get; set; } = new();
        public int TotalInscritos { get; set; }
        public double ProgresoPromedio { get; set; }

        public async Task OnGetAsync()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return;

            CursosDelProfesor = await _context.Cursos
                .Include(c => c.Inscripciones)
                .Where(c => c.InstructorId == usuario.Id)
                .ToListAsync();

            TotalInscritos = CursosDelProfesor.Sum(c => c.Inscripciones.Count);

            var totalProgresos = await _context.ProgresosLecciones
                .Include(p => p.Leccion)
                .ThenInclude(l => l.Curso)
                .Where(p => p.Leccion.Curso.InstructorId == usuario.Id)
                .ToListAsync();

            int totalCompletados = totalProgresos.Count(p => p.Visto);
            ProgresoPromedio = totalProgresos.Count > 0
                ? (double)totalCompletados / totalProgresos.Count * 100
                : 0;
        }
    }
}
