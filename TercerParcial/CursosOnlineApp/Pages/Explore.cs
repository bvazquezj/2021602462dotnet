using System.Diagnostics;
using CursosOnlineApp.Data;
using CursosOnlineApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CursosOnlineApp.Pages;

public class ExploreModel : PageModel
{
    private readonly ILogger<ExploreModel> _logger;
    private readonly AplicationDbContext _context;
    private readonly ApplicationUser _user;
    public int DuracionTotalCurso { get; set; }

    public ExploreModel(ILogger<ExploreModel> logger, AplicationDbContext context, ApplicationUser user)
    {
        _logger = logger;
        _context = context;
        _user = user;
    }

    // Propiedad pública para exponer los cursos a la vista
    public List<Curso> Cursos { get; set; } = new();

    // Carga los cursos de la base de datos al obtener la página
    public async Task OnGetAsync()
    {
        Cursos = await _context.Cursos
            .AsNoTracking()
            .ToListAsync();
            
        DuracionTotalCurso = Cursos.Sum(c => c.Lecciones.Sum(l => l.DuracionMinutos));
    }
}