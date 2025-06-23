using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Profesores
{
    [Authorize(Roles = "Profesor")]
    public class EstadisticasModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EstadisticasModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int TotalCursos { get; set; }
        public int TotalInscritos { get; set; }
        public double ProgresoPromedio { get; set; }

        public List<CursoInfo> CursosConMasInscritos { get; set; } = new();
        public List<CursoInfo> CursosConMejorProgreso { get; set; } = new();

        public class CursoInfo
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; } = "";
            public int Inscritos { get; set; }
            public double Progreso { get; set; }
        }

        public async Task OnGetAsync()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return;

            var cursos = await _context.Cursos
                .Include(c => c.Inscripciones)
                .Where(c => c.InstructorId == usuario.Id)
                .ToListAsync();

            TotalCursos = cursos.Count;
            TotalInscritos = cursos.Sum(c => c.Inscripciones.Count);

            var progresos = await _context.ProgresosLecciones
                .Include(p => p.Leccion)
                .ThenInclude(l => l.Curso)
                .Where(p => p.Leccion.Curso.InstructorId == usuario.Id)
                .ToListAsync();

            int totalCompletados = progresos.Count(p => p.Visto);
            ProgresoPromedio = progresos.Count > 0 ? (double)totalCompletados / progresos.Count * 100 : 0;

            // Cursos con más inscritos (top 5)
            CursosConMasInscritos = cursos
                .OrderByDescending(c => c.Inscripciones.Count)
                .Take(5)
                .Select(c => new CursoInfo
                {
                    CursoId = c.Id,
                    Titulo = c.Titulo,
                    Inscritos = c.Inscripciones.Count,
                    Progreso = 0 // lo calcularemos abajo
                })
                .ToList();

            // Calcular progreso por curso
            foreach (var cursoInfo in CursosConMasInscritos)
            {
                var cursoProgresos = progresos.Where(p => p.Leccion.CursoId == cursoInfo.CursoId).ToList();
                int completadosCurso = cursoProgresos.Count(p => p.Visto);
                cursoInfo.Progreso = cursoProgresos.Count > 0
                    ? (double)completadosCurso / cursoProgresos.Count * 100
                    : 0;
            }

            // Cursos con mejor progreso (top 5)
            CursosConMejorProgreso = CursosConMasInscritos
                .OrderByDescending(c => c.Progreso)
                .Take(5)
                .ToList();
        }
    }
}
