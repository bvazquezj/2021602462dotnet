using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;

namespace CursosOnlineApp.Pages.Estudiante
{
    [Authorize(Roles = "Estudiante")]
    public class ProgresoModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgresoModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<CursoProgresoViewModel> CursosConProgreso { get; set; } = new();

        public class CursoProgresoViewModel
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; } = string.Empty;
            public int TotalLecciones { get; set; }
            public int LeccionesCompletadas { get; set; }
            public int Porcentaje => TotalLecciones == 0 ? 0 : (int)((LeccionesCompletadas * 100) / TotalLecciones);
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            // Obtener inscripciones con cursos y sus lecciones
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Curso)
                    .ThenInclude(c => c.Lecciones)
                .Where(i => i.UsuarioId == user.Id)
                .ToListAsync();

            foreach (var inscripcion in inscripciones)
            {
                var curso = inscripcion.Curso;
                int totalLecciones = curso.Lecciones.Count;

                // Obtener lecciones vistas del usuario para este curso
                var leccionesVistas = await _context.ProgresosLecciones
                    .Where(pl => pl.UsuarioId == user.Id
                                 && curso.Lecciones.Select(l => l.Id).Contains(pl.LeccionId)
                                 && pl.Visto)
                    .CountAsync();

                CursosConProgreso.Add(new CursoProgresoViewModel
                {
                    CursoId = curso.Id,
                    Titulo = curso.Titulo,
                    TotalLecciones = totalLecciones,
                    LeccionesCompletadas = leccionesVistas
                });
            }
        }
    }
}
