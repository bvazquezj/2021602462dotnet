using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Lecciones;

[Authorize(Roles = "Estudiante,Profesor")]
public class VerModel : PageModel
{
    private readonly AplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public VerModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Leccion Leccion { get; set; } = null!;
    public Curso Curso => Leccion.Curso;
    public int LeccionesCompletadas { get; set; }
    public int TotalLecciones => Curso.Lecciones?.Count ?? 0;
    public int PorcentajeProgreso => TotalLecciones == 0 ? 0 : (int)Math.Round((double)LeccionesCompletadas / TotalLecciones * 100);

    public int? LeccionAnteriorId { get; set; }
    public int? LeccionSiguienteId { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Leccion = await _context.Lecciones
            .Include(l => l.Curso)
                .ThenInclude(c => c.Lecciones.OrderBy(l => l.Orden))
            .FirstOrDefaultAsync(l => l.Id == id);

        if (Leccion == null) return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        if (await _userManager.IsInRoleAsync(user, "Estudiante"))
        {
            var estaInscrito = await _context.Inscripciones
                .AnyAsync(i => i.CursoId == Curso.Id && i.UsuarioId == user.Id);

            if (!estaInscrito) return Forbid();

            LeccionesCompletadas = await _context.ProgresosLecciones
                .CountAsync(p => p.UsuarioId == user.Id && p.Leccion.CursoId == Curso.Id && p.Visto);
        }

        // Determinar anterior y siguiente
        var orden = Leccion.Orden ?? 0;
        LeccionAnteriorId = Curso.Lecciones.FirstOrDefault(l => l.Orden == orden - 1)?.Id;
        LeccionSiguienteId = Curso.Lecciones.FirstOrDefault(l => l.Orden == orden + 1)?.Id;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var leccion = await _context.Lecciones
            .Include(l => l.Curso)
            .ThenInclude(c => c.Lecciones)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (leccion == null) return NotFound();

        var yaExiste = await _context.ProgresosLecciones
            .AnyAsync(p => p.LeccionId == id && p.UsuarioId == user.Id);

        if (!yaExiste)
        {
            var progreso = new ProgresoLeccion
            {
                UsuarioId = user.Id,
                LeccionId = id,
                Visto = true
            };

            _context.ProgresosLecciones.Add(progreso);

            // Marcar curso completado si ya terminó todas las lecciones
            var total = leccion.Curso.Lecciones.Count;
            var completadas = await _context.ProgresosLecciones
                .CountAsync(p => p.UsuarioId == user.Id && p.Leccion.CursoId == leccion.CursoId && p.Visto);

            if (completadas + 1 == total)
            {
                var inscripcion = await _context.Inscripciones
                    .FirstOrDefaultAsync(i => i.CursoId == leccion.CursoId && i.UsuarioId == user.Id);

                if (inscripcion != null)
                    inscripcion.Completado = true;
            }

            await _context.SaveChangesAsync();
        }

        return new JsonResult(new { message = "Lección marcada como vista" });
    }
}

