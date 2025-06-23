using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages.Cursos;

[Authorize(Roles = "Profesor")]
public class EditarModel : PageModel
{
    private readonly AplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public EditarModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Curso Curso { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Curso = await _context.Cursos.FindAsync(id);
        if (Curso == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null || Curso.InstructorId != user.Id)
            return Forbid();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var cursoEnDb = await _context.Cursos.FindAsync(Curso.Id);
        if (cursoEnDb == null)
            return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (user == null || cursoEnDb.InstructorId != user.Id)
            return Forbid();

        // Actualizar campos
        cursoEnDb.Titulo = Curso.Titulo;
        cursoEnDb.Descripcion = Curso.Descripcion;
        cursoEnDb.Categoria = Curso.Categoria;
        cursoEnDb.Nivel = Curso.Nivel;
        cursoEnDb.Precio = Curso.Precio;
        cursoEnDb.ImagenUrl = Curso.ImagenUrl;

        await _context.SaveChangesAsync();

        return RedirectToPage("Detalle", new { id = cursoEnDb.Id });
    }
}
