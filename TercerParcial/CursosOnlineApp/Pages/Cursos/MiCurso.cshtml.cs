// MiCurso.cshtml.cs
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Estudiante,Profesor")]  // Ajusta roles según sea necesario
public class MiCursoModel : PageModel
{
    private readonly AplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public MiCursoModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public Curso Curso { get; set; } = null!;
    public bool EstaInscrito { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToPage("/Account/Login");

        Curso = await _context.Cursos
            .Include(c => c.Lecciones.OrderBy(l => l.Orden))
            .FirstOrDefaultAsync(c => c.Id == id);

        if (Curso == null)
            return NotFound();

        EstaInscrito = await _context.Inscripciones
            .AnyAsync(i => i.CursoId == id && i.UsuarioId == user.Id);

        if (!EstaInscrito)
            return Forbid(); // O redirige a detalles con mensaje

        return Page();
    }
}
