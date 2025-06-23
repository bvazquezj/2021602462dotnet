using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Cursos
{
    public class IndexModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Curso> CursosDelProfesor { get; set; } = new();
        public List<Curso> CursosInscritos { get; set; } = new();
        public List<Curso> CursosDisponibles { get; set; } = new();

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Profesor"))
            {
                CursosDelProfesor = await _context.Cursos
                    .Where(c => c.InstructorId == user.Id)
                    .Include(c => c.Inscripciones)
                    .ToListAsync();
            }

            if (roles.Contains("Estudiante"))
            {
                var cursosInscritosIds = await _context.Inscripciones
                    .Where(i => i.UsuarioId == user.Id)
                    .Select(i => i.CursoId)
                    .ToListAsync();

                CursosInscritos = await _context.Cursos
                    .Where(c => cursosInscritosIds.Contains(c.Id))
                    .ToListAsync();

                CursosDisponibles = await _context.Cursos
                    .Where(c => !cursosInscritosIds.Contains(c.Id))
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostInscribirAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Account/Login");

            var yaInscrito = await _context.Inscripciones
                .AnyAsync(i => i.CursoId == id && i.UsuarioId == user.Id);

            if (!yaInscrito)
            {
                _context.Inscripciones.Add(new Inscripcion
                {
                    CursoId = id,
                    UsuarioId = user.Id,
                    FechaInscripcion = DateTime.Now,
                    Completado = false
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); // recarga la página
        }
    }
}