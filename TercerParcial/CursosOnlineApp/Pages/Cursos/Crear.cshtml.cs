using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CursosOnlineApp.Pages.Cursos;

[Authorize(Roles = "Profesor")]
public class CrearModel : PageModel
{
    private readonly AplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CrearModel(AplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public Curso Curso { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("Modelo inválido"); // Revisa la consola o pon breakpoint
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            Console.WriteLine("Usuario no autenticado");
            return Unauthorized();
        }

        try
        {
            Curso.InstructorId = user.Id;
            Curso.FechaCreacion = DateTime.Now;

            _context.Cursos.Add(Curso);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Cursos/Detalle", new { id = Curso.Id });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar: {ex.Message}");
            return Page();
        }
       
    }
}