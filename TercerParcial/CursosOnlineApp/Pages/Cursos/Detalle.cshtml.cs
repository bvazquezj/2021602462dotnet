using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CursosOnlineApp.Pages.Cursos
{
    [Authorize]
    public class DetalleModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public Curso Curso { get; set; } = null!;
        public bool PuedeAgregarLeccion { get; set; } = false;

        public bool EsEstudiante { get; set; } = false;
        public bool EstaInscrito { get; set; } = false;

        public int DuracionTotalCurso { get; set; }
        public List<int> LeccionesVistasIds { get; set; } = new();

        public DetalleModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Curso = await _context.Cursos
                .Include(c => c.Lecciones)
                .Include(c => c.Inscripciones)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new Exception("Curso no encontrado");

            DuracionTotalCurso = Curso.Lecciones.Sum(l => l.DuracionMinutos);

            var user = await _userManager.GetUserAsync(User);
            PuedeAgregarLeccion = user != null && user.Id == Curso.InstructorId;

            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                EsEstudiante = roles.Contains("Estudiante");
            }
            if (EsEstudiante)
            {
                EstaInscrito = Curso.Inscripciones.Any(i => i.UsuarioId == user.Id);

                if (EstaInscrito)
                {
                    LeccionesVistasIds = await _context.ProgresosLecciones
                        .Where(p => p.UsuarioId == user.Id && p.Visto && p.Leccion.CursoId == Curso.Id)
                        .Select(p => p.LeccionId)
                        .ToListAsync();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostInscribirAsync(int id) // id = cursoId
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            // Validar si ya está inscrito para no duplicar
            var inscripcion = await _context.Inscripciones
                .FirstOrDefaultAsync(i => i.CursoId == id && i.UsuarioId == user.Id);

            if (inscripcion == null)
            {
                inscripcion = new Inscripcion
                {
                    CursoId = id,
                    UsuarioId = user.Id,
                    FechaInscripcion = DateTime.Now,
                    Completado = false
                };
                _context.Inscripciones.Add(inscripcion);
                await _context.SaveChangesAsync();
            }

            // Redirigir a la página de contenido del curso
            return RedirectToPage("/Cursos/Detalle", new { id = id });
        }
    }
}

