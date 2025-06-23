using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;

namespace CursosOnlineApp.Pages.DashBoard
{
    [Authorize(Roles = "Estudiante")]
    public class EstudianteModel : PageModel
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<CursoProgreso> Cursos { get; set; } = new();
        public List<Notificacion> Notificaciones { get; set; } = new();
        public List<Curso> CursosRecomendados { get; set; } = new();

        public EstudianteModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return;

            // Obtener IDs de cursos en los que el usuario está inscrito
            var cursosInscritosIds = await _context.Inscripciones
                .Where(i => i.UsuarioId == user.Id)
                .Select(i => i.CursoId)
                .ToListAsync();

            // Obtener cursos donde NO está inscrito
            CursosRecomendados = await _context.Cursos
                .Where(c => !cursosInscritosIds.Contains(c.Id))
                .Take(5) // Opcional: limitar la cantidad de cursos recomendados
                .ToListAsync();

            // Traer inscripciones con cursos y lecciones (solo IDs para eficiencia)
            var inscripciones = await _context.Inscripciones
                .Where(i => i.UsuarioId == user.Id)
                .Include(i => i.Curso)
                    .ThenInclude(c => c.Lecciones)
                .ToListAsync();

            foreach (var inscripcion in inscripciones)
            {
                var curso = inscripcion.Curso;
                int totalLecciones = curso.Lecciones.Count;

                if (totalLecciones == 0)
                {
                    Cursos.Add(new CursoProgreso
                    {
                        Id = curso.Id,
                        Nombre = curso.Titulo,
                        Porcentaje = 0
                    });
                    continue;
                }

                // Obtener Ids de lecciones del curso
                var leccionIds = curso.Lecciones.Select(l => l.Id).ToList();

                // Contar las lecciones vistas por el usuario para este curso
                int leccionesVistas = await _context.ProgresosLecciones
                    .Where(pl => pl.UsuarioId == user.Id && pl.Visto && leccionIds.Contains(pl.LeccionId))
                    .CountAsync();

                int porcentaje = (int)Math.Round((leccionesVistas * 100.0) / totalLecciones);

                Cursos.Add(new CursoProgreso
                {
                    Id = curso.Id,
                    Nombre = curso.Titulo,
                    Porcentaje = porcentaje
                });
            }

            // Simulación de notificaciones (puedes reemplazarlo con BD real)
            Notificaciones = new List<Notificacion>
            {
                new Notificacion
                {
                    Titulo = "Clase en vivo hoy a las 18:00",
                    Mensaje = "No olvides conectarte a la clase de Python Intermedio.",
                    Fecha = DateTime.Now.AddHours(-2)
                },
                new Notificacion
                {
                    Titulo = "Nuevo mensaje de tu instructor",
                    Mensaje = "Revisa el material adicional en la sección de recursos.",
                    Fecha = DateTime.Now.AddDays(-1)
                }
            };
        }
    }
}


public class CursoProgreso
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int Porcentaje { get; set; }
}

public class Notificacion
{
    public string Titulo { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}
