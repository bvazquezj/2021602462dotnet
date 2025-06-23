using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Pages.Lecciones;

[Authorize(Roles = "Profesor")]
public class EliminarModel : PageModel
{
    private readonly AplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public EliminarModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Leccion Leccion { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Leccion = await _context.Lecciones.FindAsync(id);
        if (Leccion == null)
            return NotFound();

        var curso = await _context.Cursos.FindAsync(Leccion.CursoId);
        var user = await _userManager.GetUserAsync(User);
        if (user == null || curso == null || curso.InstructorId != user.Id)
            return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        Leccion = await _context.Lecciones.FindAsync(id);
        if (Leccion == null)
            return NotFound();

        var curso = await _context.Cursos.FindAsync(Leccion.CursoId);
        var user = await _userManager.GetUserAsync(User);
        if (user == null || curso == null || curso.InstructorId != user.Id)
            return Forbid();

        _context.Lecciones.Remove(Leccion);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Cursos/Detalle", new { id = curso.Id });
    }
}
